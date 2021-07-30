#region 
using MediatR;
using Domain.DTO;
using Application;
using Domain.Model;
using Domain.DTO.Node;
using Application.Nodes;
using Application.Users;
using System.Diagnostics;
using Application.Helpers;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
#endregion

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForSeed : ControllerBase
    {

        #region Fields
        private readonly ILogger<ForSeed> _logger;
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        #endregion

        #region Ctor

        public ForSeed(
              UserManager<AppUser> userManager
            , ILogger<ForSeed> logger
            , IMediator mediator)
        {
            _userManager = userManager;
            _logger = logger;
            _mediator = mediator;
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult> SeedUserToNood(UserToNodeDto userToNodeDto)
        {

            #region register user
            RegisterDto registerDto = new();

            userToNodeDto.Name = StringGenerator.RandomString();

            registerDto.AccountBalance = 10000000;
            registerDto.Email = $"{userToNodeDto.Name}@gmail.com";
            registerDto.FirstName = userToNodeDto.Name;
            registerDto.UserName = userToNodeDto.Name;
            registerDto.LastName = userToNodeDto.Name;
            registerDto.Password = "Pa$$w0rd";
            registerDto.PhonNumber = "09016895741";
            List<string> roles = new();
            roles.Add("Customer");
            registerDto.Roles = roles;

            var user = MapUserHelper.MapAppUser(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            await _userManager.AddToRolesAsync(user, registerDto.Roles);


            #endregion

            var parentNode = await _mediator
                .Send(new FirstOrDefaultNodeAsync.Query(x => x.LeftUserId == null || x.RightUserId == null));


            userToNodeDto.IntroductionCode = parentNode.IntroductionCode;

            if (parentNode is null)
                return BadRequest();

            UserFinancialDTO userFinancialDTO = new();
            userFinancialDTO.AmountInPackage = 3000000;
            userFinancialDTO.FinancialPackageId = 1;

            CreateNodeDto createNodeDto = new();
            createNodeDto.IntroductionCode = user.IntroductionCode;
            createNodeDto.UserFinancialDTO = userFinancialDTO;

            if (parentNode.LeftUserId == null)
            {
                var res = await CreateNode(isLeft: true, parentNode.AppUser, user, createNodeDto);

                if (res)
                    return Ok();
                return BadRequest();
            }
            else if (parentNode.RightUserId == null)
            {
                var res = await CreateNode(isLeft: false, parentNode.AppUser, user, createNodeDto);

                if (res)
                    return Ok();
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }

        }



        #region helper

        private Node MapNode(AppUser user, AppUser parent, CreateNodeDto createNodeDto) =>
            new Node()
            {
                AppUser = user,
                UserId = user.Id,
                LeftUserId = null,
                RightUserId = null,
                IsCalculate = false,
                ParentId = parent.Id,
                IntroductionCode = user.IntroductionCode,
                TotalMoneyInvested = createNodeDto.UserFinancialDTO.AmountInPackage
            };


        private async Task<bool> CreateNode(
              bool isLeft
            , AppUser parentUser
            , AppUser curentUser
            , CreateNodeDto createNodeDto)
        {

            var curentNode = MapNode(curentUser, parentUser, createNodeDto);

            //this is a helper method for create user financial package . location : Application -> Helpers
            var res = await UserFinancialPackageHelper
                .CreateUserFinancialPackage(
                      curentUser
                    , createNodeDto.UserFinancialDTO
                    , _mediator);

            if (!res)
                return false;

            await _mediator.Send(new CreateNodeAsync.Command(curentNode));

            if (isLeft)
                parentUser.Node.LeftUserId = curentUser.Id;
            else
                parentUser.Node.RightUserId = curentUser.Id;

            if (parentUser.Node.LeftUserId == parentUser.Node.RightUserId)
                return false;


            //Update Parent Node 
            await _mediator.Send(new UpdateNodeAsync.Command(parentUser.Node));

            await _userManager.AddToRoleAsync(curentUser, "node");



            var parentNode = parentUser.Node;

            parentNode.TotalMoneyInvestedBySubsets += createNodeDto.UserFinancialDTO.AmountInPackage;

            parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

            await _mediator.Send(new UpdateNodeAsync.Command(parentNode));

            do
            {
                if (!curentNode.IsCalculate)
                {
                    //this is the logined user (curent user) parent`s parent !
                    parentNode = await _mediator.Send(new FindNodeByUserIdAsync.Query(parentNode.ParentId));

                    //If the current user is an Admin child(in one step); This condition applies
                    if (parentNode is null)
                    {
                        await _mediator.Send(new Save.Command());
                        return true;
                    }

                    parentNode.TotalMoneyInvestedBySubsets += curentNode.TotalMoneyInvested;

                    parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                    parentNode.AppUser.CommissionPaid = false;

                    //Update Parent Node
                    await _mediator.Send(new UpdateNodeAsync.Command(parentNode));
                    await _mediator.Send(new UpdateUserAsync.Command(parentNode.AppUser));
                }
                else
                    continue;

            } while (parentNode.ParentId is not null);

            await _mediator.Send(new Save.Command());

            return true;
        }


        private async Task<decimal> MinimumSubBranch(Node node)
        {

            var leftNode = await _mediator.Send(new FindNodeByUserIdAsync.Query(node.LeftUserId));
            var rightNode = await _mediator.Send(new FindNodeByUserIdAsync.Query(node.RightUserId));

            if (rightNode is null)
                return 0;

            if (leftNode.LeftUserId is null && rightNode.LeftUserId is null)
            {
                return
                    leftNode.TotalMoneyInvested > rightNode.TotalMoneyInvested ?
                    rightNode.TotalMoneyInvested : leftNode.TotalMoneyInvested;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null && 
                !leftNode.IsCalculate && !rightNode.IsCalculate)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested >
                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested ?

                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested
                    : leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null &&
                leftNode.IsCalculate && !rightNode.IsCalculate)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets  >
                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested ?

                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested
                    : leftNode.TotalMoneyInvestedBySubsets;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null &&
                !leftNode.IsCalculate && rightNode.IsCalculate)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested >
                    rightNode.TotalMoneyInvestedBySubsets  ?

                    rightNode.TotalMoneyInvestedBySubsets 
                    : leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null &&
                leftNode.IsCalculate && rightNode.IsCalculate)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets  > rightNode.TotalMoneyInvestedBySubsets ?
                    rightNode.TotalMoneyInvestedBySubsets : leftNode.TotalMoneyInvestedBySubsets ;
            }
            else if (leftNode.LeftUserId is not null || rightNode.LeftUserId is not null)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets > rightNode.TotalMoneyInvestedBySubsets ?
                    rightNode.TotalMoneyInvestedBySubsets : leftNode.TotalMoneyInvestedBySubsets;
            }

            else
                return 0;
        }

        #endregion


        [HttpPost("{num}")]
        public async Task MainMethod(int num)
        {
            var count = num;

            var watch = new Stopwatch();
            watch.Start();

            UserToNodeDto userToNodeDto = new();
            for (int i = 0; i < count; i++)
            {
                var res = await SeedUserToNood(userToNodeDto);
                if (res == BadRequest())
                    _logger.LogInformation("not create");
            }

            watch.Stop();
            _logger.LogInformation($"time to add {count} user : {watch.ElapsedMilliseconds} ms");
        }

    }
}

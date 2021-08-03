#region using
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

        [HttpPost("{count}")]
        public async Task<ActionResult> SeedUserToNood(int count, UserToNodeDto userToNodeDto)
        {



            List<List<AppUser>> userusers = new();
            List<List<Node>> nodenodes = new();

            for (int i = 0; i < count; i++)
            {
                List<AppUser> users = new();
                List<Node> nodes = new();

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

                #region dto
                UserFinancialDTO userFinancialDTO = new();
                userFinancialDTO.AmountInPackage = 3000000;
                userFinancialDTO.FinancialPackageId = 2;

                CreateNodeDto createNodeDto = new();
                createNodeDto.IntroductionCode = user.IntroductionCode;
                createNodeDto.UserFinancialDTO = userFinancialDTO;
                #endregion


                if (parentNode.LeftUserId == null)
                {
                    var curentNode = MapNode(user, parentNode.AppUser, createNodeDto);

                    //this is a helper method for create user financial package . location : Application -> Helpers
                    var res = await UserFinancialPackageHelper
                        .CreateUserFinancialPackage(
                              user
                            , createNodeDto.UserFinancialDTO
                            , _mediator);

                    if (!res)
                        return BadRequest();

                    await _mediator.Send(new CreateNodeAsync.Command(curentNode));

                    parentNode.AppUser.Node.LeftUserId = user.Id;

                    if (parentNode.AppUser.Node.LeftUserId == parentNode.AppUser.Node.RightUserId)
                        return BadRequest();

                    //Update Parent Node 
                    await _mediator.Send(new UpdateNodeAsync.Command(parentNode.AppUser.Node));

                    await _userManager.AddToRoleAsync(user, "node");



                    parentNode = parentNode.AppUser.Node;

                    parentNode.TotalMoneyInvestedBySubsets += createNodeDto.UserFinancialDTO.AmountInPackage;

                    parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                    nodes.Add(parentNode);

                    do
                    {
                        if (!curentNode.IsCalculate)
                        {
                            //this is the logined user (curent user) parent`s parent !
                            parentNode = await _mediator
                                .Send(new FindNodeByUserIdAsync.Query(parentNode.ParentId));

                            //If the current user is an Admin child(in one step); This condition applies
                            if (parentNode is null)
                                break;

                            parentNode.TotalMoneyInvestedBySubsets += curentNode.TotalMoneyInvested;

                            parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                            parentNode.AppUser.CommissionPaid = false;

                            users.Add(parentNode.AppUser);
                            nodes.Add(parentNode);

                        }
                        else
                            continue;

                    } while (parentNode.ParentId is not null);

                    await _mediator.Send(new UpdateListNodeAsync.Command(nodes));
                    await _mediator.Send(new UpdateListUserAsync.Command(users));
                }
                else if (parentNode.RightUserId == null)
                {
                    var curentNode = MapNode(user, parentNode.AppUser, createNodeDto);

                    //this is a helper method for create user financial package . location : Application -> Helpers
                    var res = await UserFinancialPackageHelper
                        .CreateUserFinancialPackage(
                              user
                            , createNodeDto.UserFinancialDTO
                            , _mediator);

                    if (!res)
                        return BadRequest();

                    await _mediator.Send(new CreateNodeAsync.Command(curentNode));

                    parentNode.AppUser.Node.RightUserId = user.Id;

                    if (parentNode.AppUser.Node.LeftUserId == parentNode.AppUser.Node.RightUserId)
                        return BadRequest();

                    //Update Parent Node 
                    await _mediator.Send(new UpdateNodeAsync.Command(parentNode.AppUser.Node));

                    await _userManager.AddToRoleAsync(user, "node");


                    parentNode = parentNode.AppUser.Node;

                    parentNode.TotalMoneyInvestedBySubsets += createNodeDto.UserFinancialDTO.AmountInPackage;

                    parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                    //await _mediator.Send(new UpdateNodeAsync.Command(parentNode));
                    nodes.Add(parentNode);

                    do
                    {
                        if (!curentNode.IsCalculate)
                        {
                            //this is the logined user (curent user) parent`s parent !
                            parentNode = await _mediator
                                .Send(new FindNodeByUserIdAsync.Query(parentNode.ParentId));

                            //If the current user is an Admin child(in one step); This condition applies
                            if (parentNode is null)
                                break;

                            parentNode.TotalMoneyInvestedBySubsets += curentNode.TotalMoneyInvested;

                            parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                            parentNode.AppUser.CommissionPaid = false;

                            users.Add(parentNode.AppUser);
                            nodes.Add(parentNode);
                        }
                        else
                            continue;

                    } while (parentNode.ParentId is not null);

                    await _mediator.Send(new UpdateListNodeAsync.Command(nodes));
                    await _mediator.Send(new UpdateListUserAsync.Command(users));
                }
                else
                {
                    return BadRequest();
                }
            }

            return Ok();
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
                    leftNode.TotalMoneyInvestedBySubsets >
                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested ?

                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested
                    : leftNode.TotalMoneyInvestedBySubsets;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null &&
                !leftNode.IsCalculate && rightNode.IsCalculate)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested >
                    rightNode.TotalMoneyInvestedBySubsets ?

                    rightNode.TotalMoneyInvestedBySubsets
                    : leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null &&
                leftNode.IsCalculate && rightNode.IsCalculate)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets > rightNode.TotalMoneyInvestedBySubsets ?
                    rightNode.TotalMoneyInvestedBySubsets : leftNode.TotalMoneyInvestedBySubsets;
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

    }
}
using Domain.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Application.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Repository;
using Domain.DTO.Node;
using Domain.DTO.FinancialDTO;
using System.Collections.Generic;
using Application;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForSeed : ControllerBase
    {

        #region Fields
        private readonly ISave _save;
        private readonly IUser _user;
        private readonly INode _node;
        private readonly ILogger<ForSeed> _logger;
        private readonly IUserFinancial _userFinancial;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFinancialPackage _financialPackage;

        #endregion

        #region Ctor

        public ForSeed(
              IUser user
            , INode node
            , IUserFinancial userFinancial
            , UserManager<AppUser> userManager
            , IFinancialPackage financialPackage, ILogger<ForSeed> logger, ISave save)
        {
            _user = user;
            _node = node;
            _userManager = userManager;
            _userFinancial = userFinancial;
            _financialPackage = financialPackage;
            _logger = logger;
            _save = save;
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


            var parentNode = await _node.FirstOrDefaultAsync(x => x.LeftUserId == null || x.RightUserId == null, c => c.AppUser);

            userToNodeDto.IntroductionCode = parentNode.IntroductionCode;

            //var parentUser = await _user
            //    .FirstOrDefaultAsync(u => u.IntroductionCode == userToNodeDto.IntroductionCode, b => b.Node);

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
                LeftUserId = null,
                RightUserId = null,
                ParentId = parent.Id,
                TotalMoneyInvested = createNodeDto.UserFinancialDTO.AmountInPackage,
                IntroductionCode = user.IntroductionCode
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
                    , _userFinancial
                    , createNodeDto.UserFinancialDTO
                    , _financialPackage);


            if (!res)
                return false;

            await _node.Create(curentNode);

            if (isLeft)
                parentUser.Node.LeftUserId = curentUser.Id;
            else
                parentUser.Node.RightUserId = curentUser.Id;

            _user.Update(parentUser);

            await _userManager.AddToRoleAsync(curentUser, "node");



            var parentNode = parentUser.Node;

            parentNode.TotalMoneyInvestedBySubsets += createNodeDto.UserFinancialDTO.AmountInPackage;

            parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

            _node.Update(parentNode);

            do
            {
                if (!curentNode.IsCalculate)
                {
                    //this is the logined user (curent user) parent`s parent !
                    parentNode = await _node
                        .FirstOrDefaultAsync(x => x.AppUser.Id == parentNode.ParentId, y => y.AppUser);

                    //If the current user is an Admin child(in one step); This condition applies
                    if (parentNode is null)
                    {
                        await _save.SaveChangeAsync();

                        return true;
                    }

                    parentNode.TotalMoneyInvestedBySubsets += curentNode.TotalMoneyInvested;

                    parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                    parentNode.AppUser.CommissionPaid = false;

                    _node.Update(parentNode);
                }
                else
                {
                    continue;
                }

            } while (parentNode.ParentId is not null);

            await _save.SaveChangeAsync();

            return true;
        }


        private async Task<decimal> MinimumSubBranch(Node node)
        {
            var leftNode = await _node.FirstOrDefaultAsync(x => x.UserId == node.LeftUserId, y => y.AppUser);
            var rightNode = await _node.FirstOrDefaultAsync(x => x.UserId == node.RightUserId, y => y.AppUser);

            if (rightNode is null)
                return 0;

            if (leftNode.LeftUserId is null && rightNode.LeftUserId is null)
            {
                return
                    leftNode.TotalMoneyInvested > rightNode.TotalMoneyInvested ?
                    rightNode.TotalMoneyInvested : leftNode.TotalMoneyInvested;
            }
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested >
                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested ?

                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested
                    : leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested;
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
                await SeedUserToNood(userToNodeDto);
            }

            watch.Stop();
            _logger.LogInformation($"time to add {count} user : {watch.ElapsedMilliseconds} ms");
        }

    }
}

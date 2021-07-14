#region using
using Domain.DTO;
using System.Linq;
using API.Services;
using Domain.Model;
using System.Text.Json;
using Application.Helpers;
using Persistence.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System;
#endregion

namespace API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AcountController : ControllerBase
    {

        #region constructor and fields

        private readonly IUser _user;
        private readonly IProfit _profit;
        private readonly ITransaction _transaction;
        private readonly IUserFinancial _userFinancial;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFinancialPackage _financialPackage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AcountController(
              IUser user
            , IProfit profit
            , IUserFinancial userFinancial
            , UserManager<AppUser> userManager
            , ITransaction transaction
            , IFinancialPackage financialPackage
            , RoleManager<IdentityRole> roleManager
            , IAuthenticationManager authenticationManager)
        {
            _user = user;
            _profit = profit;
            _userManager = userManager;
            _roleManager = roleManager;
            _transaction = transaction;
            _userFinancial = userFinancial;
            _financialPackage = financialPackage;
            _authenticationManager = authenticationManager;
        }

        #endregion

        #region Action mthods

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("all filds required");

            if (!await _authenticationManager.ValidateUser(loginDto))
            {
                return BadRequest("Email or password is invalid");
            }

            var currentUser = await _authenticationManager.GetCurrentUser(loginDto);
            var userObjectDto = await CreateUserObject(currentUser);
            var json = JsonSerializer.Serialize(userObjectDto);

            return Ok(json);

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {

            #region Validation

            if (!ModelState.IsValid)
                return BadRequest("all filds required");

            if (await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
                return BadRequest("Email Taken");

            if (await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName))
                return BadRequest("Username Taken");

            foreach (var role in registerDto.Roles)
            {
                if (await _roleManager.RoleExistsAsync(role) is false)
                {
                    return BadRequest($"{role} : Does not exist !");
                }
            }

            #endregion


            #region Related to the subset

            var branchHead = await GetParentUserByIntroductionCode(registerDto.ParrentIntroductionCode);

            if (branchHead is null)
                return BadRequest("Introduction code is not available");

            var subcategoryUsers = GetSubBranchUsers(branchHead.Id);

            if (subcategoryUsers.Count() >= 2)
                return BadRequest("This introduction code is complete");

            #endregion

            var user = MapUserHelper.MapAppUser(registerDto, branchHead);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            #region result error

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            #endregion

            await _userManager.AddToRolesAsync(user, registerDto.Roles);

            var json = JsonSerializer.Serialize(user);

            #region user financial package

            UserFinancialPackage userFinancialPackage = new();
            userFinancialPackage.AmountInPackage = registerDto.AccountBalance;
            userFinancialPackage.FinancialPackage = await _financialPackage.GetByIdAsync(registerDto.FinancialPackageId);
            userFinancialPackage.User = user;

            if (DontHaveEnoughMony(userFinancialPackage, user))
            {
                var pureAccountBalance = GetPureAccountBalance(user);
                return BadRequest($"You dont have enough mony! your pure account balance : {pureAccountBalance}");
            }

            await _userFinancial.CreateAsync(userFinancialPackage);

            #endregion

            string branchHeadId = branchHead.Id;

            do
            {
                var subBranchs = GetSubBranchUsers(branchHeadId);

                if (subBranchs.Count() == 2)
                {
                    //Branch head profit is 10% of its lowest sub-branch
                    decimal branchHeadProfit = MinimumAmountMonyInSubSets(subBranchs) * 10 / 100;

                    await CreateTransaction(branchHead, branchHeadProfit);

                    await CreateProfit(branchHead, branchHeadProfit);

                    UpdateUserAmountBalance(branchHead, branchHeadProfit);

                }

                var parentBranchHead = await GetParentUserById(branchHead.ParentId);
                branchHead = parentBranchHead;
                if (parentBranchHead is not null)
                    branchHeadId = parentBranchHead.Id;
                else
                    branchHeadId = null;
            }
            while (branchHeadId is not null);

            return Ok(json);
        }




        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetCorrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return await CreateUserObject(user);
        }
        #endregion

        #region Helper

        private async Task CreateProfit(AppUser parentUser, decimal branchHeadProfit)
        {
            Profit profit = new();
            profit.User = parentUser;
            profit.ProfitAmount = branchHeadProfit;

            await _profit.CreateAsync(profit);
        }

        private void UpdateUserAmountBalance(AppUser parentUser, decimal branchHeadProfit)
        {
            parentUser.AccountBalance += branchHeadProfit;
            _user.UpdateAsync(parentUser);
        }

        private async Task<TransactionDTo> CreateTransaction(AppUser parentUser, decimal branchHeadProfit)
        {
            TransactionDTo transactionDTo = new();
            transactionDTo.EmailTargetAccount = parentUser.Email;
            transactionDTo.Amount = branchHeadProfit;

            var transaction = MapTransactionHelper.MapTransaction(parentUser, transactionDTo);

            await _transaction.CreateAsync(transaction);
            return transactionDTo;
        }

        private decimal MinimumAmountMonyInSubSets(List<AppUser> subBranchUsers) =>
            subBranchUsers[0].AccountBalance > subBranchUsers[1].AccountBalance ?
                subBranchUsers[1].AccountBalance : subBranchUsers[0].AccountBalance;


        private List<AppUser> GetSubBranchUsers(string parentId) =>
             _user.Where(u => u.ParentId == parentId).ToList();

        private async Task<AppUser> GetParentUserByIntroductionCode(string IntroductionCode) =>
            await _user
                .FirstOrDefaultAsync(u => u.IntroductionCode == IntroductionCode);

        private async Task<AppUser> GetParentUserById(string id)
        {
            var res = await _user
                .FirstOrDefaultAsync(u => u.Id == id);

            return res is null ? null : res;

        }




        private decimal GetPureAccountBalance(AppUser user)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user);

            return user.AccountBalance - sumAmountInPackages;
        }

        private bool DontHaveEnoughMony(UserFinancialPackage userFinancial, AppUser user)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user);

            if (sumAmountInPackages == 0)
            {
                return user.AccountBalance < userFinancial.AmountInPackage ? true : false;
            }

            return user.AccountBalance < sumAmountInPackages + userFinancial.AmountInPackage ? true : false;
        }

        private async Task<UserDto> CreateUserObject(AppUser user) =>
           new UserDto()
           {
               Id = user.Id,
               Name = user.FirstName + " " + user.LastName,
               Email = user.Email,
               Token = await _authenticationManager.CreateToken()
           };

        private decimal SumAmountInPackages(AppUser user)
        {
            decimal sumAmountInPackages = 0;

            var userFinancials = _userFinancial
                .Where(x => x.UserId == user.Id);

            foreach (var finance in userFinancials)
            {
                sumAmountInPackages += finance.AmountInPackage;
            }
            return sumAmountInPackages;
        }

    }

    #endregion

}

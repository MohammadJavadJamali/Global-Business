using Domain.Model;
using Persistence.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserFinancialController : ControllerBase
    {
        #region constructor and fields
        private readonly IUserFinancial _userFinancial;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFinancialPackage _financialPackage;

        public UserFinancialController(
              IUserFinancial userFinancial
            , UserManager<AppUser> userManager
            , IFinancialPackage financialPackage)
        {
            _userManager = userManager;
            _userFinancial = userFinancial;
            _financialPackage = financialPackage;
        }

        #endregion

        #region Action Method
        [HttpPost]
        public async Task<ActionResult<UserFinancialPackage>> CreateUserFinance(UserFinancialDTO financialDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("All filds are required");

            var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            if (currentUser == null)
                return BadRequest("Email in not exist!");


            var financialPackageFromDb = await _financialPackage.GetByIdAsync(financialDTO.FinancialPackageId);
            if(financialPackageFromDb == null)
                return BadRequest("invalid financial package");

            if (DontHaveEnoughMony(financialDTO, currentUser))
            {
                var pureAccountBalance = GetPureAccountBalance(currentUser);

                return BadRequest($"You dont have enough mony! your pure account balance : {pureAccountBalance}");
            }


            var userFinance = new UserFinancialPackage();

            //Dates are set in the create function in the repository
            userFinance.AmountInPackage = financialDTO.AmountInPackage;
            userFinance.FinancialPackage = financialPackageFromDb;
            userFinance.User = currentUser;


            try
            {
                await _userFinancial.CreateAsync(userFinance);

                return Ok(financialDTO);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Helper

        private bool DontHaveEnoughMony(UserFinancialDTO financialDTO, AppUser user)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user);

            if (sumAmountInPackages == 0)
            {
                return user.AccountBalance < financialDTO.AmountInPackage ? true : false;
            }

            return user.AccountBalance < sumAmountInPackages + financialDTO.AmountInPackage ? true : false;
        }

        /// <summary>
        /// get account balance that financial packages have been reduced
        /// </summary>
        /// <param name="financialDTO"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private decimal GetPureAccountBalance(AppUser user)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user);

            return user.AccountBalance - sumAmountInPackages;
        }

        /// <summary>
        ///Returns total deposits of user financial packages
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
        #endregion

    }
}

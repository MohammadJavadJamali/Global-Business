using Domain.Model;
using Application.Helpers;
using Application.Repository;
using System.Security.Claims;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Node")]
    public class UserFinancialController : ControllerBase
    {
        private readonly ISave _save;
        private readonly INode _node;
        private readonly IUserFinancial _userFinancial;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFinancialPackage _financialPackage;

        public UserFinancialController(
              INode node
            , IUserFinancial userFinancial
            , UserManager<AppUser> userManager
            , IFinancialPackage financialPackage, ISave save)
        {
            _node = node;
            _userManager = userManager;
            _userFinancial = userFinancial;
            _financialPackage = financialPackage;
            _save = save;
        }

        [HttpPost]
        public async Task<ActionResult<UserFinancialPackage>> CreateUserFinance(UserFinancialDTO financialDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest("All filds are required");


            var currentUser = await 
                _userManager
                .Users
                .Include(n => n.Node)
                .FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));

            if (currentUser == null)
                return BadRequest("Email in not exist!");

            var res = await UserFinancialPackageHelper
                .CreateUserFinancialPackage(currentUser, _userFinancial, financialDTO, _financialPackage);

            var node = await _node.GetByUserId(currentUser.Id);
            var parentNode = await _node.GetByUserId(node.ParentId);

            node.TotalMoneyInvested += financialDTO.AmountInPackage;
            parentNode.TotalMoneyInvestedBySubsets += financialDTO.AmountInPackage;

            await _node.UpdateAsync(node);
            await _node.UpdateAsync(parentNode);

            await _save.SaveChangeAsync();

            if (res)
                return Ok();
            else
                return BadRequest("Something is wrong !");

        }

        private bool DontHaveEnoughMony(UserFinancialDTO financialDTO, AppUser user)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user);

            if (sumAmountInPackages == 0)
            {
                return user.AccountBalance < financialDTO.AmountInPackage ? true : false;
            }

            return user.AccountBalance < sumAmountInPackages + financialDTO.AmountInPackage ? true : false;
        }

        private decimal GetPureAccountBalance(AppUser user)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user);

            return user.AccountBalance - sumAmountInPackages;
        }

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
}

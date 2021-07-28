using MediatR;
using System.Linq;
using Domain.Model;
using Application.Nodes;
using Application.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Node")]
    public class UserFinancialController : ControllerBase
    {

        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        #endregion

        #region ctor
        public UserFinancialController(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }
        #endregion

        #region Action Method
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

            //var node = await _node.FirstOrDefaultAsync(x => x.UserId == currentUser.Id, u => u.AppUser);

            //var parentNode = await _node
            //    .FirstOrDefaultAsync(x => x.LeftUserId == node.UserId || x.RightUserId == node.UserId, u => u.AppUser);
            

            if (currentUser == null)
                return BadRequest("Email in not exist!");

            var res = await UserFinancialPackageHelper
                .CreateUserFinancialPackage(currentUser, financialDTO, _mediator);



            //var node = await _node.GetByUserId(currentUser.Id);
            //var parentNode = await _node.GetByUserId(node.ParentId);

            var node = await _mediator.Send(new FindNodeByUserIdAsync.Query(currentUser.Id));
            var parentNode = await _mediator.Send(new FindNodeByUserIdAsync.Query(node.ParentId));

            node.TotalMoneyInvested += financialDTO.AmountInPackage;
            parentNode.TotalMoneyInvestedBySubsets += financialDTO.AmountInPackage;

            //await _node.UpdateAsync(node);
            //await _node.UpdateAsync(parentNode);
            await _mediator.Send(new UpdateNodeAsync.Command(node));
            await _mediator.Send(new UpdateNodeAsync.Command(parentNode));

            if (res)
                return Ok();
            else
                return BadRequest("Something is wrong !");

        }
        #endregion

        #region Helper

        private async Task<bool> DontHaveEnoughMony(UserFinancialDTO financialDTO, AppUser user)
        {
            decimal sumAmountInPackages = await SumAmountInPackages(user);

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
        private async Task<decimal> GetPureAccountBalance(AppUser user)
        {
            decimal sumAmountInPackages = await SumAmountInPackages(user);

            return user.AccountBalance - sumAmountInPackages;
        }

        /// <summary>
        ///Returns total deposits of user financial packages
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<decimal> SumAmountInPackages(AppUser user)
        {
            decimal sumAmountInPackages = 0;

            //var userFinancials = _userFinancial
            //    .Where(x => x.UserId == user.Id);

            var userFinancials = await _mediator.Send(new GetAllUserFinancialPackagesAsync.Query());
            userFinancials = userFinancials.Where(x => x.UserId == user.Id).ToList();

            foreach (var finance in userFinancials)
            {
                sumAmountInPackages += finance.AmountInPackage;
            }
            return sumAmountInPackages;
        }
        #endregion

    }
}

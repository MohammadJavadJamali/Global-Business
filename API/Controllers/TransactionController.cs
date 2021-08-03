#region using
using System;
using MediatR;
using Domain.DTO;
using Domain.Model;
using Application.Users;
using Application.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
#endregion

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public TransactionController(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        #endregion

        #region Action mthods

        [HttpPost]
        public async Task<ActionResult> CreateTransaction(TransactionDTo transactionDTo)
        {

            if (!ModelState.IsValid)
                return BadRequest("All filds are requird!");

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

                //var targetUser = await _user
                //    .FirstOrDefaultAsync(u => u.Email == transactionDTo.EmailTargetAccount, null);
                var targetUser = await _mediator
                    .Send(new FindUserByIntroductionCodeAsync.Query(transactionDTo.EmailTargetAccount));

                if (currentUser.NormalizedEmail == targetUser.Email.ToUpper())
                {
                    await TransactionHelper.CreateTransaction(currentUser, transactionDTo.Amount, _mediator);
                }
                else
                {
                    await TransactionHelper.CreateTransaction(currentUser, -1 * transactionDTo.Amount, _mediator);

                    await TransactionHelper.CreateTransaction(targetUser, transactionDTo.Amount, _mediator);
                }

                return Ok();

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}

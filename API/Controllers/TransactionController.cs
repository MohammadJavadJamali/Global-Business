using Domain.DTO;
using Domain.Model;
using Application.Helpers;
using Application.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ISave _save;
        private readonly IUser _user;
        private readonly ITransaction _transaction;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(
              IUser user
            , ITransaction transaction
            , UserManager<AppUser> userManager, ISave save)
        {
            _user = user;
            _transaction = transaction;
            _userManager = userManager;
            _save = save;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransaction(TransactionDTo transactionDTo)
        {
            if (!ModelState.IsValid)
                return BadRequest("All filds are requird!");

            try
            {
                var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

                var targetUser = await _user
                    .FirstOrDefaultAsync(u => u.Email == transactionDTo.EmailTargetAccount, null);

                if (currentUser.NormalizedEmail == targetUser.Email.ToUpper())
                {

                    TransactionHelper.CreateTransaction(_user, currentUser, transactionDTo.Amount, _transaction);
                    await _save.SaveChangeAsync();
                }
                else
                {

                    TransactionHelper.CreateTransaction(_user, currentUser, -1 * transactionDTo.Amount, _transaction);
                 
                    TransactionHelper.CreateTransaction(_user, targetUser, transactionDTo.Amount, _transaction);
                    
                    await _save.SaveChangeAsync();
                }

                return Ok();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

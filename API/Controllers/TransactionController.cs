using System;
using Domain.DTO;
using Domain.Model;
using Persistence.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
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
        #region constructor and fields

        private readonly IUser _user;
        private readonly ITransaction _transaction;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(
              IUser user
            , ITransaction transaction
            , UserManager<AppUser> userManager)
        {
            _user = user;
            _transaction = transaction;
            _userManager = userManager;
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
                var targetUser = _user.GetUserByEmail(transactionDTo.EmailTargetAccount);

                if (currentUser.NormalizedEmail == targetUser.Email.ToUpper())
                {
                    var transaction = new Transaction();

                    transaction.Amount = transactionDTo.Amount;
                    transaction.InitialBalance = currentUser.AccountBalance;
                    transaction.FinalBalance = currentUser.AccountBalance + transactionDTo.Amount;
                    transaction.User = currentUser;

                    await _transaction.CreateAsync(transaction);

                    currentUser.AccountBalance = currentUser.AccountBalance + transactionDTo.Amount;

                    _user.UpdateAsync(currentUser);

                }
                else
                {
                    #region Map Objects
                    var transactionForCurrentUser = new Transaction();
                    transactionForCurrentUser.Amount = -1 * transactionDTo.Amount;
                    transactionForCurrentUser.InitialBalance = currentUser.AccountBalance;
                    transactionForCurrentUser.FinalBalance = currentUser.AccountBalance - transactionDTo.Amount;
                    transactionForCurrentUser.EmailTargetAccount = transactionDTo.EmailTargetAccount;
                    transactionForCurrentUser.User = currentUser;

                    var transactionForTagetUser = new Transaction();
                    transactionForTagetUser.Amount = transactionDTo.Amount;
                    transactionForTagetUser.InitialBalance = targetUser.AccountBalance;
                    transactionForTagetUser.FinalBalance = targetUser.AccountBalance + transactionDTo.Amount;
                    transactionForTagetUser.EmailTargetAccount = currentUser.Email;
                    transactionForTagetUser.User = targetUser;
                    #endregion

                    await _transaction.CreateAsync(transactionForCurrentUser);
                    await _transaction.CreateAsync(transactionForTagetUser);

                    currentUser.AccountBalance -= transactionDTo.Amount; 
                    targetUser.AccountBalance += transactionDTo.Amount;

                    _user.UpdateAsync(currentUser);
                    _user.UpdateAsync(targetUser);
                }

                return Ok();

            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion

    }
}

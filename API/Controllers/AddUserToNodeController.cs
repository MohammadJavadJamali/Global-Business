#region usings
using MediatR;
using Domain.Model;
using Domain.DTO.Node;
using Application.Users;
using Application.Nodes;
using Application.Helpers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
#endregion

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class AddUserToNodeController : ControllerBase
    {
        #region Ctor
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public AddUserToNodeController(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }
        #endregion

        #region Methods

        [HttpPost("{id}")]
        public async Task<Node> FindNodeById(int id)
        {
            var node = await _mediator.Send(new FindNodeByIdAsync.Query(id));
            return node;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNode(CreateNodeDto createNodeDto)
        {

            //Get parent of curent user by introduction code
            var parentUser = await _mediator
                .Send(new FindUserByIntroductionCodeAsync.Query(createNodeDto.IntroductionCode));

            #region validation

            if (parentUser is null)
                return BadRequest("Introduction code is invalid !");

            #endregion

            var curentUser = await _userManager.Users.Include(u => u.Node)
                .FirstOrDefaultAsync(f => f.Email == User.FindFirstValue(ClaimTypes.Email));

            #region validation

            if (curentUser is null)
                return BadRequest("user not found !");

            if (curentUser.Node is not null)
                if (curentUser.Node.ParentId is not null)
                    return BadRequest("You in set !");

            #endregion

            #region Create Node

            if (parentUser.Node.LeftUserId is null)
            {
                var res = await CreateNode(isLeft: true, parentUser, curentUser, createNodeDto);

                if (res)
                    return Ok();

                return BadRequest("Something is wrong !");
            }
            else if (parentUser.Node.RightUserId is null)
            {
                var res = await CreateNode(isLeft: false, parentUser, curentUser, createNodeDto);

                if (res)
                    return Ok();

                return BadRequest("Something is wrong !");
            }
            else
            {
                return BadRequest("Introduction code is complete");
            }

            #endregion

        }

        #endregion

        #region helper

        private Node MapNode(AppUser user, AppUser parent, CreateNodeDto createNodeDto) =>
            new Node()
            {
                AppUser = user,
                UserId = user.Id,
                LeftUserId = null,
                RightUserId = null,
                ParentId = parent.Id,
                IsCalculate = false,
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
                        return true;

                    parentNode.TotalMoneyInvestedBySubsets += curentNode.TotalMoneyInvested;

                    parentNode.MinimumSubBrachInvested = await MinimumSubBranch(parentNode);

                    parentNode.AppUser.CommissionPaid = false;

                    //Update Parent Node
                    await _mediator.Send(new UpdateNodeAsync.Command(parentNode));
                }
                else
                    continue;

            } while (parentNode.ParentId is not null);

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
            else if (leftNode.LeftUserId is not null && rightNode.LeftUserId is not null)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested >
                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested ?

                    rightNode.TotalMoneyInvestedBySubsets + rightNode.TotalMoneyInvested
                    : leftNode.TotalMoneyInvestedBySubsets + leftNode.TotalMoneyInvested;
            }
            else if(leftNode.LeftUserId is not null || rightNode.LeftUserId is not null)
            {
                return
                    leftNode.TotalMoneyInvestedBySubsets > rightNode.TotalMoneyInvestedBySubsets ?
                    rightNode.TotalMoneyInvestedBySubsets : leftNode.TotalMoneyInvestedBySubsets ;
            }
            else
                return 0;
        }

        #endregion

    }
}

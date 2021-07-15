using Domain.Model;
using Domain.DTO.Node;
using Persistence.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AddUserToNodeController : ControllerBase
    {

        private readonly IUser _user;
        private readonly UserManager<AppUser> _userManager;
        private readonly INodeRepository _nodeRepository;

        public AddUserToNodeController(
              IUser user
            , INodeRepository nodeRepository
            , UserManager<AppUser> userManager)
        {
            _user = user;
            _nodeRepository = nodeRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNode(CreateNodeDto createNodeDto)
        {

            var parentUser = await _user
                .FirstOrDefaultAsync(u => u.IntroductionCode == createNodeDto.IntroductionCode, b => b.Node);

            if (parentUser.Node is null)
                return BadRequest("parent user does not exist in set !");
            


            var curentUser = await _userManager.Users.Include(u => u.Node)
                .FirstOrDefaultAsync(f => f.Email == User.FindFirstValue(ClaimTypes.Email));

            if (curentUser.Node.ParentId is not null)
                return BadRequest("You in set !");



            if (parentUser.Node.LeftUserId == null)
            {
                var node = MapNode(curentUser, parentUser);
                await _nodeRepository.CreateAsync(node);

                parentUser.Node.LeftUserId = curentUser.Id;
                _user.UpdateAsync(parentUser);

                return Ok();
            }
            else if(parentUser.Node.RightUserId == null)
            {
                var node = MapNode(curentUser, parentUser);
                await _nodeRepository.CreateAsync(node);

                parentUser.Node.RightUserId = curentUser.Id;
                _user.UpdateAsync(parentUser);

                return Ok();
            }
            else
            {
                return BadRequest("Introduction code in complete");
            }

        }

        private Node MapNode(AppUser user, AppUser parent)
        {
            Node node = new();
            node.AppUser = user;
            node.ParentId = parent.Id;
            node.LeftUserId = null;
            node.RightUserId = null;

            return node;
        }

    }
}

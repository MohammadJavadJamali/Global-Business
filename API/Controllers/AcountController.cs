#region using
using Domain.DTO;
using API.Services;
using Domain.Model;
using System.Text.Json;
using Application.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
#endregion

namespace API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AcountController : ControllerBase
    {
        #region Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authenticationManager;

        #endregion

        #region Ctor

        public AcountController(
              UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IAuthenticationManager authenticationManager)
        {
            
            _userManager = userManager;
            _roleManager = roleManager;
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


            var user = MapUserHelper.MapAppUser(registerDto);

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

            return Ok(json);
        }

        #endregion

        #region Helper

        private async Task<UserDto> CreateUserObject(AppUser user) =>
           new UserDto()
           {
               Id = user.Id,
               Name = user.FirstName + " " + user.LastName,
               Email = user.Email,
               Token = await _authenticationManager.CreateToken()
           };
        #endregion
    }
}

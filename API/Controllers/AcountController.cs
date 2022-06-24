using Domain.DTO;
using API.Services;
using Domain.Model;
using System.Text.Json;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AcountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AcountController(
              UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IAuthenticationManager authenticationManager)
        {
            
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationManager = authenticationManager;
        }

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

            var user = MapUserHelper.MapAppUser(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            
            await _userManager.AddToRolesAsync(user, registerDto.Roles);

            var json = JsonSerializer.Serialize(user);

            return Ok(json);
        }

        private async Task<UserDto> CreateUserObject(AppUser user) =>
           new UserDto()
           {
               Id = user.Id,
               Name = user.FirstName + " " + user.LastName,
               Email = user.Email,
               Token = await _authenticationManager.CreateToken()
           };
    }
}

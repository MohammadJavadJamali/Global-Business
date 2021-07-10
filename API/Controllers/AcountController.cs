using System;
using AutoMapper;
using Domain.DTO;
using Domain.Model;
using API.Services;
using System.Text.Json;
using System.Security.Claims;
using System.Threading.Tasks;
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
        #region constructor and fields

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authenticationManager;
        public AcountController(
              IMapper mapper
            , UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IAuthenticationManager authenticationManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
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
            if (!ModelState.IsValid)
                return BadRequest("all filds required");

            if (await _userManager.Users.AnyAsync(u => u.Email == registerDto.Email))
                return BadRequest("Email Taken");

            if (await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName))
                return BadRequest("Username Taken");

            var user = _mapper.Map<AppUser>(registerDto);

            foreach (var role in registerDto.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return BadRequest($"{role} : Does not exist !");
                }
            }

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetCorrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return await CreateUserObject(user);
        }
        #endregion

        #region Helper
        /// <summary>
        /// Create UserDTO from AppUser 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserDTO</returns>
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

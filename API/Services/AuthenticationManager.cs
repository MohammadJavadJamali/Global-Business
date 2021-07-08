using System;
using Domain.DTO;
using System.Text;
using Domain.Model;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private AppUser _user;
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationManager(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task<AppUser> GetCurrentUser(LoginDto loginDto) =>
            await _userManager.FindByEmailAsync(loginDto.Email);

        public async Task<bool> ValidateUser(LoginDto loginDto)
        {
            _user = await GetCurrentUser(loginDto);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _config.GetSection("jwt");

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, _user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id));
            claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
                                                                  
            var roles = await _userManager.GetRolesAsync(_user);  

            foreach (var role in roles)                           
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

    }
}

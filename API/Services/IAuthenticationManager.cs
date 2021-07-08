using Domain.DTO;
using Domain.Model;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAuthenticationManager
    {
        Task<AppUser> GetCurrentUser(LoginDto loginDto);
        Task<bool> ValidateUser(LoginDto loginDto);
        Task<string> CreateToken();
    }
}

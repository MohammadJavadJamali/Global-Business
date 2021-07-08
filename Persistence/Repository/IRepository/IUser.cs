using Domain.Model;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IUser
    {
        AppUser GetUserByEmail(string email);

        IEnumerable<AppUser> GetAll();

        void UpdateAsync(AppUser entity);
    }
}
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IUser
    {

        Task<IEnumerable<AppUser>> GetAll(Expression<Func<AppUser, bool>> filter = null);

        IEnumerable<AppUser> Where(Expression<Func<AppUser, bool>> expression);

        Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression);

        void UpdateAsync(AppUser entity);
    }
}
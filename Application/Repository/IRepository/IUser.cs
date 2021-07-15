using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IUser
    {

        Task<IEnumerable<AppUser>> GetAll(Expression<Func<AppUser, bool>> filter = null
            , Expression<Func<AppUser, object>> expression = null);

        IEnumerable<AppUser> Where(Expression<Func<AppUser, bool>> expression);

        /// <summary>
        /// first expression for FirstOrDefult query and secound for include query 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="criteria"></param>
        /// <returns>app user with included to said entity</returns>
        Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression, Expression<Func<AppUser, object>> criteria);

        void UpdateAsync(AppUser entity);
    }
}
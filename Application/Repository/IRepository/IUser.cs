using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface IUser
    {

        Task<IEnumerable<AppUser>> GetAll(Expression<Func<AppUser, bool>> filter = null
            , Expression<Func<AppUser, object>> include = null);

        IEnumerable<AppUser> Where(Expression<Func<AppUser, bool>> expression);

        /// <summary>
        /// first expression for FirstOrDefult query and secound for include query 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        /// <returns>app user with included to said entity</returns>
        Task<AppUser> FirstOrDefaultAsync(
            Expression<Func<AppUser, bool>> expression
            , Expression<Func<AppUser, object>> include = null);

        Task UpdateAsync(AppUser entity);

        void Update(AppUser entity);
    }
}
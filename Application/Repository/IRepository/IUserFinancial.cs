using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IUserFinancial
    {
        Task CreateAsync(UserFinancialPackage entity);

        Task<UserFinancialPackage> GetById(int id);

        /// <summary>
        /// delete entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        Task DeleteAsync(UserFinancialPackage entity);

        IEnumerable<UserFinancialPackage> Where(Expression<Func<UserFinancialPackage, bool>> expression);
    }
}
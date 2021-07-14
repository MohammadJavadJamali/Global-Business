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

        //Task<IEnumerable<UserFinancialPackage>> GetAll();

        Task DeleteAsync(int id);

        IEnumerable<UserFinancialPackage> Where(Expression<Func<UserFinancialPackage, bool>> expression);
    }
}
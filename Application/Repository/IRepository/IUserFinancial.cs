using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface IUserFinancial
    {
        Task CreateAsync(UserFinancialPackage entity);

        Task Create(UserFinancialPackage entity);

        Task UpdateAsync(UserFinancialPackage entity);

        void Update(UserFinancialPackage entity);

        Task<UserFinancialPackage> GetById(int id);

        Task DeleteAsync(int id);

        Task DeleteAsync(UserFinancialPackage entity);

        IEnumerable<UserFinancialPackage> Where(Expression<Func<UserFinancialPackage, bool>> expression);
    }
}
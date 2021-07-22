using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface IFinancialPackage
    { 
        Task CreateAsync(FinancialPackage entity);
        Task Create(FinancialPackage entity);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<FinancialPackage>> GetAll(
              Expression<Func<FinancialPackage, bool>> expression = null
            , Expression<Func<FinancialPackage, object>> include = null);

        Task<FinancialPackage> FirstOrDefaultAsync(
              Expression<Func<FinancialPackage, bool>> expression
            , Expression<Func<FinancialPackage, object>> include = null);

        Task<FinancialPackage> GetByIdAsync(int id);

        Task RemoveAsync(FinancialPackage entity);

        Task UpdateAsync(FinancialPackage entity);
        void Update(FinancialPackage entity);

    }
}
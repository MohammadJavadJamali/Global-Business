using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IFinancialPackage
    {
        Task CreateAsync(FinancialPackage entity);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<FinancialPackage>> GetAll(Expression<Func<FinancialPackage, bool>> expression = null);

        Task<FinancialPackage> FirstOrDefaultAsync(Expression<Func<FinancialPackage, bool>> expression
            , Expression<Func<FinancialPackage, object>> criteria);

        Task<FinancialPackage> GetByIdAsync(int id);

        void RemoveAsync(FinancialPackage entity);

        void UpdateAsync(FinancialPackage entity);

    }
}
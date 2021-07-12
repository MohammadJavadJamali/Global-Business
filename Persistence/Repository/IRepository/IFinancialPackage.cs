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

        Task<bool> Delete(int id);

        Task<IEnumerable<FinancialPackage>> GetAll(Expression<Func<FinancialPackage, bool>> expression = null);

        Task<FinancialPackage> GetByIdAsync(int id);

        void RemoveAsync(FinancialPackage entity);

        void UpdateAsync(FinancialPackage entity);

    }
}
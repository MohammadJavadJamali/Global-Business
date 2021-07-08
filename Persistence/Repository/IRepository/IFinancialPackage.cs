using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IFinancialPackage
    {
        Task CreateAsync(FinancialPackage entity);

        Task<bool> Delete(int id);

        IEnumerable<FinancialPackage> GetAll();

        Task<FinancialPackage> GetByIdAsync(int id);

        void RemoveAsync(FinancialPackage entity);

        void UpdateAsync(FinancialPackage entity);

    }
}
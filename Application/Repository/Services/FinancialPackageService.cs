//Remember that the DepositProfit class does not use repository
using System;
using System.Linq;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public class FinancialPackageService : IFinancialPackage
    {
        private readonly IRepository<FinancialPackage> _repository;

        public FinancialPackageService(IRepository<FinancialPackage> repository)
        {
            _repository = repository;
        }


        public virtual async Task CreateAsync(FinancialPackage entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task Create(FinancialPackage entity)
        {
            await _repository.Create(entity);
        }

        public void Update(FinancialPackage entity)
        {
            _repository.Update(entity);
        }

        public virtual async Task UpdateAsync(FinancialPackage entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<FinancialPackage>> GetAll(
            Expression<Func<FinancialPackage, bool>> expression = null,
            Expression<Func<FinancialPackage, object>> include = null)
        {
            var financial = await _repository.GetAll(expression, include);
            return financial.Where(x => x.IsDeleted == false);
        }

        public virtual async Task<FinancialPackage> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await _repository.DeleteAsync(id);
            return res;
        }

        public async Task RemoveAsync(FinancialPackage entity)
        {
            entity.IsDeleted = true;

            await _repository.UpdateAsync(entity);
        }

        public async Task<FinancialPackage> FirstOrDefaultAsync(
              Expression<Func<FinancialPackage, bool>> expression
            , Expression<Func<FinancialPackage, object>> include = null) =>
                await _repository.FirstOrDefaultAsync(expression, include);
    }
}

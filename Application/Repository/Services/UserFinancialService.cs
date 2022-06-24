using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public class UserFinancialService : IUserFinancial
    {
        private readonly IRepository<UserFinancialPackage> _repository;

        public UserFinancialService(IRepository<UserFinancialPackage> repository)
        {
            _repository = repository;
        }

        public virtual async Task CreateAsync(UserFinancialPackage entity)
        {
            entity.ChoicePackageDate = DateTime.Now;
            entity.EndFinancialPackageDate = DateTime.Now.AddMonths(entity.FinancialPackage.Term);
            entity.DayCount = (entity.EndFinancialPackageDate - entity.ChoicePackageDate).Days;

            await _repository.CreateAsync(entity);

        }

        public virtual async Task Create(UserFinancialPackage entity)
        {
            await _repository.Create(entity);
        }

        public void Update(UserFinancialPackage entity)
        {
            _repository.Update(entity);
        }

        public virtual async Task UpdateAsync(UserFinancialPackage entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<UserFinancialPackage> GetById(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public async Task DeleteAsync(UserFinancialPackage entity) =>
            await _repository.DeleteAsync(entity);

        public IEnumerable<UserFinancialPackage> Where(Expression<Func<UserFinancialPackage, bool>> expression) =>
            _repository.Where(expression);

    }
}

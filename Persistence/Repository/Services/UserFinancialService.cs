//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public class UserFinancialService : IUserFinancial
    {
        #region constructor and fields

        private readonly IRepository<UserFinancialPackage> _repository;
        public UserFinancialService(IRepository<UserFinancialPackage> repository)
        {
            _repository = repository;
        }

        #endregion

        public virtual async Task CreateAsync(UserFinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            entity.ChoicePackageDate = DateTime.Now;
            entity.EndFinancialPackageDate = DateTime.Now.AddMonths(entity.FinancialPackage.Term);

            await _repository.CreateAsync(entity);

        }

        public async Task<UserFinancialPackage> GetById(int id) =>
            await _repository.GetByIdAsync(id);

        //public async Task<IEnumerable<UserFinancialPackage>> GetAll(Expression<Func<UserF, bool>> expression = null) =>
        //    await _repository.GetAll(expression);

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public IEnumerable<UserFinancialPackage> Find(Expression<Func<UserFinancialPackage, bool>> expression) =>
            _repository.Find(expression);



    }
}

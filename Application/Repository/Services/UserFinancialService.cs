//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public class UserFinancialService : IUserFinancial
    {
        #region Fields

        private readonly IRepository<UserFinancialPackage> _repository;

        #endregion

        #region Ctro

        public UserFinancialService(IRepository<UserFinancialPackage> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public virtual async Task CreateAsync(UserFinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            entity.ChoicePackageDate = DateTime.Now;
            entity.EndFinancialPackageDate = DateTime.Now.AddMonths(entity.FinancialPackage.Term);
            entity.DayCount = (entity.EndFinancialPackageDate - entity.ChoicePackageDate).Days;

            await _repository.CreateAsync(entity);

        }


        public virtual async Task Create(UserFinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            #region comment
            //entity.ChoicePackageDate = DateTime.Now;
            //entity.EndFinancialPackageDate = DateTime.Now.AddMonths(entity.FinancialPackage.Term);
            //entity.DayCount = (entity.EndFinancialPackageDate - entity.ChoicePackageDate).Days;
            #endregion 

            await _repository.Create(entity);

        }

        public void Update(UserFinancialPackage entity)
        {
            if (entity is null)
                throw new ArgumentNullException();

            _repository.Update(entity);
        }

        public virtual async Task UpdateAsync(UserFinancialPackage entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.UpdateAsync(entity);
        }

        public async Task<UserFinancialPackage> GetById(int id) =>
            await _repository.GetByIdAsync(id);

        //public async Task<IEnumerable<UserFinancialPackage>> GetAll(Expression<Func<UserF, bool>> expression = null) =>
        //    await _repository.GetAll(expression);

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task DeleteAsync(UserFinancialPackage entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public IEnumerable<UserFinancialPackage> Where(Expression<Func<UserFinancialPackage, bool>> expression) =>
            _repository.Where(expression);

        #endregion
    }
}

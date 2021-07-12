//Remember that the DepositProfit class does not use repository
using System;
using System.Linq;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public class FinancialPackageService : IFinancialPackage
    {
        #region constructor and fields
        private readonly IRepository<FinancialPackage> _repository;

        // Main Constructor
        public FinancialPackageService(IRepository<FinancialPackage> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        //Create new entity
        public virtual async Task CreateAsync(FinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.CreateAsync(entity);
        }


        // Update given entity
        public virtual void UpdateAsync(FinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }


        // Get's All entities 
        public async Task<IEnumerable<FinancialPackage>> GetAll(Expression<Func<FinancialPackage, bool>> expression = null)
        {
            var financial = await _repository.GetAll(expression);
            return financial.Where(x => x.IsDeleted == false);
        }

        // Get an entity by id
        public virtual async Task<FinancialPackage> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return await _repository.GetByIdAsync(id);
        }


        /// <summary>
        /// Deletes the entity that passed to it from database
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> Delete(int id)
        {
            try
            {
                var res = await _repository.DeleteAsync(id);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Remove by remove state
        public void RemoveAsync(FinancialPackage entity)
        {
            try
            {
                entity.IsDeleted = true;

                _repository.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

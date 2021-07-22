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
        #region Fields
        private readonly IRepository<FinancialPackage> _repository;
        #endregion

        #region Ctro
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

        public async Task Create(FinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.Create(entity);
        }

        public void Update(FinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }

        // Update given entity
        public virtual async Task UpdateAsync(FinancialPackage entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.UpdateAsync(entity);
        }


        // Get's All entities 
        public async Task<IEnumerable<FinancialPackage>> GetAll(
            Expression<Func<FinancialPackage, bool>> expression = null,
            Expression<Func<FinancialPackage, object>> include = null)
        {
            var financial = await _repository.GetAll(expression, include);
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
        public async Task<bool> DeleteAsync(int id)
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
        public async Task RemoveAsync(FinancialPackage entity)
        {
            try
            {
                entity.IsDeleted = true;

                await _repository.UpdateAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FinancialPackage> FirstOrDefaultAsync(
              Expression<Func<FinancialPackage, bool>> expression
            , Expression<Func<FinancialPackage, object>> include = null) =>
                await _repository.FirstOrDefaultAsync(expression, include);


        #endregion
    }
}

using System;
using System.Linq;
using Domain.Model;
using System.Threading.Tasks;
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

            _repository.UpdateAsync(entity);
        }


        // Get's All entities 
        public IEnumerable<FinancialPackage> GetAll() =>
            _repository.GetAll().Where(f => f.IsDeleted == false);

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

                _repository.UpdateAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

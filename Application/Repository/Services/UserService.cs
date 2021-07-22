//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;


namespace Application.Repository
{
    public class UserService : IUser
    {
        #region Fields

        private readonly IRepository<AppUser> _repository;
        
        #endregion

        #region Ctro
        
        public UserService(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<AppUser>> GetAll(
              Expression<Func<AppUser, bool>> filter = null
            , Expression<Func<AppUser, object>> include = null) =>
                await _repository.GetAll(filter, include);

        

        public async Task<AppUser> FirstOrDefaultAsync(
              Expression<Func<AppUser, bool>> expression
            , Expression<Func<AppUser, object>> include = null) =>
            await _repository.FirstOrDefaultAsync(expression, include);


        public IEnumerable<AppUser> Where(Expression<Func<AppUser, bool>> expression)
        {
            return _repository.Where(expression);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task UpdateAsync(AppUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// update without saving
        /// </summary>
        /// <param name="entity"></param>
        public void Update(AppUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }

        #endregion

    }
}

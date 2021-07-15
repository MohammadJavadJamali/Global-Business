//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;


namespace Persistence.Repository
{
    public class UserService : IUser
    {
        #region constructor and fields
        private readonly IRepository<AppUser> _repository;
        public UserService(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        #endregion


        public async Task<IEnumerable<AppUser>> GetAll(Expression<Func<AppUser, bool>> filter = null
                                                        , Expression<Func<AppUser, object>> expression = null) =>
            await _repository.GetAll(filter, expression);

        

        public async Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> expression, Expression<Func<AppUser, object>> criteria) =>
            await _repository.FirstOrDefaultAsync(expression, criteria);


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
        public virtual void UpdateAsync(AppUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }

    }
}

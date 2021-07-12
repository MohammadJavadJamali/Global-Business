//Remember that the DepositProfit class does not use repository
using System;
using System.Linq;
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


        public async Task<IEnumerable<AppUser>> GetAll(Expression<Func<AppUser, bool>> filter = null) =>
            await _repository.GetAll(filter);


        public AppUser FindUser(Expression<Func<AppUser, bool>> expression)
        {
            return _repository.Find(expression).First();
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

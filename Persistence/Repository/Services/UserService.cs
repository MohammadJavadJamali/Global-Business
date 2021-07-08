using System;
using System.Linq;
using Domain.Model;
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


        public IEnumerable<AppUser> GetAll() =>
            _repository.GetAll();

        public AppUser GetUserByEmail(string email) =>
             _repository.GetAll().FirstOrDefault(u => u.Email == email);

        // Update given entity
        public virtual void UpdateAsync(AppUser entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.UpdateAsync(entity);
        }


        //public virtual async Task<AppUser> GetUserById(string id) =>
        //    await _repository.GetByIdAsync(id);

    }
}

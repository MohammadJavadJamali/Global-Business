using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;


namespace Application.Repository
{
    public class UserService : IUser
    {
        private readonly IRepository<AppUser> _repository;

        public UserService(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppUser>> GetAll(
              Expression<Func<AppUser, bool>> filter = null
            , Expression<Func<AppUser, object>> include = null) =>
                await _repository.GetAll(filter, include);

        public async Task<AppUser> FirstOrDefaultAsync(
              Expression<Func<AppUser, bool>> expression
            , Expression<Func<AppUser, object>> include = null) =>
            await _repository.FirstOrDefaultAsync(expression, include);

        public IEnumerable<AppUser> Where(Expression<Func<AppUser, bool>> expression) =>
            _repository.Where(expression);

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task UpdateAsync(AppUser entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public void Update(AppUser entity)
        {
            _repository.Update(entity);
        }
    }
}

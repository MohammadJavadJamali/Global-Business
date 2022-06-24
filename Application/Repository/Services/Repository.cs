using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _entities;
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        protected virtual DbSet<T> Entity
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await Entity.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new DbUpdateException();
            }
            return entity;
        }
        public async Task Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                 await Entity.AddAsync(entity);
            }
            catch (Exception)
            {
                throw new DbUpdateException();
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new DbUpdateException();
            }
            return entity;
        }
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entity.Update(entity);
            }
            catch (Exception)
            {
                throw new DbUpdateException();
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            try
            {
                var entity = await GetByIdAsync(id);

                await DeleteAsync(entity);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                Entity.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Entity.Where(expression);
        }
        
        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> include = null)
        {

            IQueryable<T> query = Entity;

            if(include is not null)
                query = query.Include(include);


            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();

        }

        public async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> include = null)
        {

            IQueryable<T> query = Entity;

            if (include is not null)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(expression);

        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await Entity.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

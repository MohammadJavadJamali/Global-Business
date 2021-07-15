//Remember that the DepositProfit class does not use repository
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties and constructor

        private DbSet<T> _entities;
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        // Gets a table
        public virtual IQueryable<T> Table => Entity;

        // Get a table with no tracking
        public virtual IQueryable<T> TableNoTracking => Entity.AsNoTracking();

        // Gets an entity set
        protected virtual DbSet<T> Entity
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception"></param>
        /// <returns> error message </returns>
        protected async Task<string> GetFullErrorTextAndRollbackEntityChangesAsync(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DataContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                await _context.SaveChangesAsync();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await Entity.AddAsync(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
            }
            return entity;
        }

        /// <summary>
        /// Update a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));


            try
            {
                Entity.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new DbUpdateException();
            }
            return entity;
        }

        /// <summary>
        /// remove record from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        //Find records that have this experssion
        public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Entity.Where(expression);
        }

        /// <summary>
        /// Get all record . you can pass parameters in string format for include method .
        /// parameters should split with ',' if you have mor than one patameter 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns>List of your entity with include field</returns>
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null
            , Expression<Func<T, object>> expression = null)
        {

            IQueryable<T> query = Entity;

            if (filter != null)
                query = query.Where(filter);

            if (expression is not null)
                query = query.Include(expression);

            return await query.ToListAsync();

        }


        /// <summary>
        /// It has a similar function to the FirstOrDefaultAsync function
        /// </summary>
        /// <param name="expression"></param>
        /// <returns> a Entity </returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> criteria = null)
        {

            IQueryable<T> query = Entity;

            if (criteria is not null)
                query = query.Include(criteria);

            return await query.FirstOrDefaultAsync(expression);
        }



        /// <summary>
        /// Find record with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(object id)
        {
            return await Entity.FindAsync(id);
        }

        //Dispose
        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}

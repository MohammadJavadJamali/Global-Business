using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        public virtual IQueryable<T> Table => Entities;

        // Get a table with no tracking
        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        // Gets an entity set
        protected virtual DbSet<T> Entities
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

            //Create
            public async Task<T> CreateAsync(T entity)
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                try
                {
                    await _context.Set<T>().AddAsync(entity);
                    _context.SaveChanges();
                }
                catch (DbUpdateException exception)
                {
                    throw new Exception(await GetFullErrorTextAndRollbackEntityChangesAsync(exception), exception);
                }
                return entity;
            }

            
            public T UpdateAsync(T entity)
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

            
                try
                {
                    _context.Set<T>().Update(entity);
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

                    _context.Set<T>().Remove(entity);
                    _context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }

            //Find records that have this experssion
            public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
            {
                return _context.Set<T>().Where(expression);
            }
            
            //Get all records 
            public IEnumerable<T> GetAll() =>
                _context.Set<T>().ToList();


            //Find record with id
            public async Task<T> GetByIdAsync(object id)
            {
                return await _context.Set<T>().FindAsync(id);
            }

            //Dispose
            public void Dispose()
            {
                _context.Dispose();
            }

        #endregion
    }
}

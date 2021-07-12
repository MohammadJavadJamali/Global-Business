﻿using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
                                     
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null);

        Task<T> GetByIdAsync(object id);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T entity);

        //remove record from database
        Task<bool> DeleteAsync(object id);

        T Update(T entity);

    }
}

using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface IUserFinancial
    {
        /// <summary>
        /// create and save at that moment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task CreateAsync(UserFinancialPackage entity);

        /// <summary>
        /// create without save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(UserFinancialPackage entity);

        /// <summary>
        /// update and save at the moment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(UserFinancialPackage entity);

        /// <summary>
        /// update without save
        /// </summary>
        /// <param name="entity"></param>
        void Update(UserFinancialPackage entity);

        Task<UserFinancialPackage> GetById(int id);

        /// <summary>
        /// delete entity from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        Task DeleteAsync(UserFinancialPackage entity);

        IEnumerable<UserFinancialPackage> Where(Expression<Func<UserFinancialPackage, bool>> expression);
    }
}
//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public class ProfitService : IProfit
    {
        #region Fields
        private readonly IRepository<Profit> _repository;
        #endregion

        #region Ctro
        public ProfitService(IRepository<Profit> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<Profit>> GetAll(
            Expression<Func<Profit, bool>> expression = null,
            Expression<Func<Profit, object>> include = null) =>
            await _repository.GetAll(expression, include);


        public async Task CreateAsync(Profit profit)
        {
            profit.ProfitDepositDate = DateTime.Now;
            await _repository.CreateAsync(profit);
        }

        /// <summary>
        /// create withiout saving
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Create(Profit entity)
        {
            entity.ProfitDepositDate = DateTime.Now;
            await _repository.Create(entity);
        }

        #endregion
    }
}

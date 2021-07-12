//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public class ProfitService : IProfit
    {
        #region constructor and fields

        private readonly IRepository<Profit> _repository;

        public ProfitService(IRepository<Profit> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Profit>> GetAll(Expression<Func<Profit, bool>> expression = null) =>
            await _repository.GetAll(expression);

        public async Task CreateAsync(Profit profit)
        {
            profit.ProfitDepositDate = DateTime.Now;
            await _repository.CreateAsync(profit);
        }

        #endregion


    }
}

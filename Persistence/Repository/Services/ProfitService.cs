using System;
using Domain.Model;
using System.Threading.Tasks;
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

        public IEnumerable<Profit> GetAll() =>
            _repository.GetAll();

        public async Task CreateAsync(Profit profit)
        {
            profit.ProfitDepositDate = DateTime.Now;
            await _repository.CreateAsync(profit);
        }

        #endregion

        #region Filters

        //public IEnumerable<Profit> GetProfitsByDateFilter(DateTime startDate, DateTime endDate)
        //{
        //    var 
        //}
        //_profitDataContext
        //.Profits
        //.Where(p => p.ProfitDepositDate >= startDate && p.ProfitDepositDate <= endDate)
        //.ToList();

        #endregion

    }
}

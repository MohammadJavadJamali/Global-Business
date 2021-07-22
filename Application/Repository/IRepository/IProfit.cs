using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface IProfit
    {
        Task CreateAsync(Profit profit);

        Task Create(Profit profit);

        Task<IEnumerable<Profit>> GetAll(
            Expression<Func<Profit, bool>> expression = null,
            Expression<Func<Profit, object>> include = null);

    }
}
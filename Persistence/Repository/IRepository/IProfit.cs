using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IProfit
    {
        Task CreateAsync(Profit profit);
        IEnumerable<Profit> GetAll();
    }
}
using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface ITransaction
    {
        Task CreateAsync(Transaction entity);
        IEnumerable<Transaction> GetAll();
        Task<Transaction> GetByIdAsync(int id);
    }
}
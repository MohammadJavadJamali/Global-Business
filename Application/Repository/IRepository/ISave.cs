using System;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface ISave : IDisposable
    {
        Task SaveChangeAsync();
    }
}

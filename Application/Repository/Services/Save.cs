using System.Threading.Tasks;

namespace Application.Repository
{
    public class Save : ISave
    {
        private readonly DataContext _context;

        public Save(DataContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Domain.Forms;
using Domain.Interfaces;

namespace Persistence.Repositories
{
    public class OrderFormRepository : IOrderFormRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<int> AddAsync(OrderForm orderForm)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderForm> GetByIdAsync(int procurementId)
        {
            return await _context.OrderForms.FindAsync(procurementId);
        }

        public Task<IEnumerable<OrderForm>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetSerialNumberCountAsync(string datePart)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderForm orderForm)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int procurementId)
        {
            throw new NotImplementedException();
        }
    }
}

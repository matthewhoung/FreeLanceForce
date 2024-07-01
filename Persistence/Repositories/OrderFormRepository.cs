using Domain.Forms;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class OrderFormRepository : IOrderFormRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderForm> GetByIdAsync(int formId)
        {
            return await _context.OrderForms.FindAsync(formId);
        }

        public async Task<IEnumerable<OrderForm>> GetAllAsync()
        {
            return await _context.OrderForms.ToListAsync();
        }

        public async Task<int> AddAsync(OrderForm orderForm)
        {
            var createdOrderForm = await _context.OrderForms.AddAsync(orderForm);
            await _context.SaveChangesAsync();
            return createdOrderForm.Entity.ProcurementId;
        }

        public async Task UpdateAsync(OrderForm orderForm)
        {
            _context.OrderForms.Update(orderForm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int procurementId)
        {
            var orderForm = await _context.OrderForms.FindAsync(procurementId);
            if (orderForm != null)
            {
                _context.OrderForms.Remove(orderForm);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetSerialNumberCountAsync(int formId, string stage, string attachType)
        {
            var partialDate = DateTime.UtcNow.ToString("MMddyyyy");
            int currentSerialCount = await _context.OrderForms
                                  .CountAsync(of => EF.Functions.Like(of.SerialNumber, $"%{partialDate}%"));

            string serialNumber = new SerialNumber().SerialNumberGenerator(formId, stage, currentSerialCount, attachType);
            return serialNumber;
        }
    }
}

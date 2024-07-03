using Application.Forms.DTOs;
using Domain.Entities.Forms;
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

    }
}

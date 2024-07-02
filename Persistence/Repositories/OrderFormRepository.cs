using Application.Forms.DTOs;
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
        //Advanced Querying
        public async Task<OrderFormDto> GetOrderFormDetailsByIdAsync(int formId)
        {
            var query = from orderform in _context.OrderForms
                        join form in _context.Forms on orderform.FormId equals form.Id
                        where orderform.FormId == formId
                        select new OrderFormDto
                        {
                            FormId = form.Id,
                            ProductId = form.ProductId,
                            Stage = form.Stage,
                            ProcurementId = orderform.ProcurementId,
                            SerialNumber = orderform.SerialNumber,
                            Title = orderform.Title,
                            Description = orderform.Description,
                            Status = orderform.Status,
                            CreatedAt = orderform.CreateAt,
                            UpdatedAt = orderform.UpdateAt
                        };

            return await query.FirstOrDefaultAsync();
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

    }
}

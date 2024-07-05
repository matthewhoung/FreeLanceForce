using Domain.Entities.Forms;
using Domain.Enums;
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

        public async Task<int> AddAsync(OrderForm orderForm)
        {
            var createdOrderForm = await _context.OrderForms.AddAsync(orderForm);
            await _context.SaveChangesAsync();
            return createdOrderForm.Entity.ProcurementId;
        }

        public async Task UpdateSignatureAsync(int formId, int userId, bool isApproved, bool isRejected, string? memo)
        {
            var orderFormSignature = await _context.OrderFormSignatures.FindAsync(formId, userId);

            if (orderFormSignature == null)
            {
                throw new ArgumentException("Signature not found.");
            }

            switch (isApproved, isRejected)
            {
                case (true, false):
                    orderFormSignature.Approve(DateTime.Now, memo);
                    await _context.SaveChangesAsync();
                    break;

                case (false, true):
                    orderFormSignature.Reject(DateTime.Now, memo);
                    await _context.SaveChangesAsync();
                    break;

                default:
                    _: throw new ArgumentException("Invalid signature status.");
            }
        }

        public async Task UpdateStatusAsync(int formId, Status status)
        {
            var orderForm = await _context.OrderForms.FindAsync(formId);
            if (orderForm != null)
            {
                orderForm.UpdateStatus(status);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTitleAsync(int formId, string title)
        {
            var orderForm = await _context.OrderForms.FindAsync(formId);
            if (orderForm != null)
            {
                orderForm.UpdateTitle(title);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateDescriptionAsync(int formId, string description)
        {
            var orderForm = await _context.OrderForms.FindAsync(formId);
            if (orderForm != null)
            {
                orderForm.UpdateDescription(description);
                await _context.SaveChangesAsync();
            }
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

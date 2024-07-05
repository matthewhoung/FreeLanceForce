using Domain.Entities.Forms;
using Domain.Interfaces;

namespace Persistence.Repositories
{
    public class PaymentFormRepository : IPaymentFormRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(PaymentForm form)
        {
            var createdForm = await _context.PaymentForms.AddAsync(form);
            await _context.SaveChangesAsync();
            return createdForm.Entity.FormId;
        }
    }
}

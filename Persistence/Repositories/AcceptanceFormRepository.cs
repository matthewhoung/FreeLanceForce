using Domain.Entities.Forms;
using Domain.Interfaces;

namespace Persistence.Repositories
{
    public class AcceptanceFormRepository : IAcceptanceFormRepository
    {
        private readonly ApplicationDbContext _context;

        public AcceptanceFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(AcceptanceForm form)
        {
            var createdForm = await _context.AcceptanceForms.AddAsync(form);
            await _context.SaveChangesAsync();
            return createdForm.Entity.FormId;
        }
    }
}

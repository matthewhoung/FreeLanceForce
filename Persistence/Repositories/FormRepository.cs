using Domain.Forms;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Form> GetByIdAsync(int id)
        {
            return await _context.Forms.FindAsync(id);
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            return await _context.Forms.ToListAsync();
        }

        public async Task<int> AddAsync(Form form)
        {
            var createdForm = await _context.Forms.AddAsync(form);
            await _context.SaveChangesAsync();
            return createdForm.Entity.Id;
        }

        public async Task UpdateAsync(Form form)
        {
            _context.Forms.Update(form);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form != null)
            {
                _context.Forms.Remove(form);
                await _context.SaveChangesAsync();
            }
        }
    }
}

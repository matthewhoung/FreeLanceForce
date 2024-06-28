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
            var formInfo = await _context.Forms.FindAsync(id);
            return formInfo;
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            var formInfos = await _context.Forms.ToListAsync();
            return formInfos;
        }

        public async Task<int> AddAsync(Form form)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var createdForm = await _context.Forms.AddAsync(form);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return createdForm.Entity.Id;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException("An error occurred while creating the form");
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var form = await _context.Forms.FindAsync(id);
                    if (form != null)
                    {
                        _context.Forms.Remove(form);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException("An error occurred while deleting the form.", ex);
                }
            }
        }

        public async Task UpdateAsync(Form form)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Entry(form).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException("An error occurred while updating the form.", ex);
                }
            }
        }
    }
}

using Application.Forms.DTOs;
using Domain.Entities.Forms;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class GenericFormRepository : IGenericFormRepository
    {
        private readonly ApplicationDbContext _context;

        public GenericFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<FormDetailDto> GetFormDetailsByIdAsync(int formId)
        {
            var query = from orderform in _context.OrderForms
                        join form in _context.Forms on orderform.FormId equals form.Id
                        where orderform.FormId == formId
                        select new FormDetailDto
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

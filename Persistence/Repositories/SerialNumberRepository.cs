using Domain.Forms;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SerialNumberRepository : ISerialNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public SerialNumberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> SerialNumberAsync(int formId, string stage, string? isAttachForm)
        {
            var partialDate = DateTime.UtcNow.ToString("MMddyyyy");
            int currentSerialCount = await _context.OrderForms
                                  .CountAsync(of => EF.Functions.Like(of.SerialNumber, $"%{partialDate}%"));

            string serialNumber = new SerialNumber().SerialNumberGenerator(formId, stage, currentSerialCount, isAttachForm);
            return serialNumber;
        }
        public async Task<string> AttachmentSerialNumberAsync(int formId, string stage, string AttachType)
        {
            throw new NotImplementedException();
        }
    }
}

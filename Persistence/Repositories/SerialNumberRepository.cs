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

        public async Task<string> GenerateSerialNumberAsync(int formId, string stage, bool? isAttach)
        {
            if (isAttach != null)
            {
                var baseSerialNumber = await GetBaseSerialNumberAsync(formId);
                return new SerialNumber().GenerateForAttachment(baseSerialNumber, isAttach.Value);
            }

            int currentSerialCount = await CountValidSerialNumbersAsync();
            string serialNumber = new SerialNumber().Generate(formId, stage, currentSerialCount);

            switch (stage)
            {
                case "OrderForm":
                    return serialNumber;
                case "AcceptanceForm":
                    var orderFormSerial = await GetSerialNumberAsync(formId, "OrderForm");
                    return new SerialNumber().TransformForStage(orderFormSerial, "OrderForm", "AcceptanceForm");
                case "PaymentForm":
                    var acceptanceFormSerial = await GetSerialNumberAsync(formId, "AcceptanceForm");
                    return new SerialNumber().TransformForStage(acceptanceFormSerial, "AcceptanceForm", "PaymentForm");
                default:
                    throw new ArgumentException($"{stage} is an invalid input.");
            }
        }

        private async Task<int> CountValidSerialNumbersAsync()
        {
            var currentDateTime = DateTime.UtcNow.ToString("MMddyyyy");
            return await _context.OrderForms
                .CountAsync(of => EF.Functions.Like(of.SerialNumber, $"%{currentDateTime}%") &&
                                  !of.SerialNumber.EndsWith("A") &&
                                  !of.SerialNumber.EndsWith("B"));
        }

        private async Task<string> GetSerialNumberAsync(int formId, string stage)
        {
            return await _context.OrderForms
                .Where(of => of.FormId == formId)
                .Select(of => of.SerialNumber)
                .FirstOrDefaultAsync();
        }

        private async Task<string> GetBaseSerialNumberAsync(int formId)
        {
            return await _context.OrderForms
                .Where(of => of.FormId == formId)
                .Select(of => of.SerialNumber)
                .FirstOrDefaultAsync();
        }
    }
}

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

        public async Task<string> SerialNumberAsync(int formId, string stage, bool? isAttach)
        {
            if (isAttach != null)
            {
                return await AttachmentSerialNumberAsync(formId, isAttach.Value);
            }

            int currentSerialCount = await CountValidSerialNumberAsync();
            string serialNumber = new SerialNumber().SerialNumberGenerator(formId, stage, currentSerialCount);

            switch (stage)
            {
                case "OrderForm":
                    return serialNumber;

                case "AcceptanceForm":
                    string acceptanceFormSerial = await GetSerialNumberAsync(formId, "OrderForm");
                    acceptanceFormSerial = acceptanceFormSerial.Replace("O", "A");
                    return acceptanceFormSerial;

                case "PaymentForm":
                    string paymentFormSerialNumber = await GetSerialNumberAsync(formId, "AcceptanceForm");
                    paymentFormSerialNumber = paymentFormSerialNumber.Replace("A", "P");
                    return paymentFormSerialNumber;

                default:
                    throw new ArgumentException($"{stage} is an invalid input.");
            }
        }

        private async Task<int> CountValidSerialNumberAsync()
        {
            var currentDateTime = DateTime.UtcNow.ToString("MMddyyyy");

            int currentSerialCount = await _context.OrderForms
                .Where(of => EF.Functions.Like(of.SerialNumber, $"%{currentDateTime}%") &&
                             !of.SerialNumber.EndsWith("A") &&
                             !of.SerialNumber.EndsWith("B"))
                .CountAsync();

            return currentSerialCount;
        }

        private async Task<string> GetSerialNumberAsync(int formId, string stage)
        {
            var orderFormNumber = await _context.OrderForms
                .Where(of => of.FormId == formId)
                .Select(of => of.SerialNumber)
                .FirstOrDefaultAsync();

            return orderFormNumber;
        }
        

        private async Task<string> AttachmentSerialNumberAsync(int formId, bool isAttach)
        {
            const string stageconst = "OrderForm";
            string targetSerialNumber = await GetSerialNumberAsync(formId, stageconst);

            switch (isAttach)
            {
                case true:
                    return $"{targetSerialNumber}-A";
                case false:
                    return $"{targetSerialNumber}-B";
                default:
                    throw new ArgumentException($"{isAttach} is an invalid input.");
            }
        }
    }
}

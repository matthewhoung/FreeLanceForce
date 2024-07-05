using Domain.Entities;
using Domain.Entities.Forms;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Services;
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

        public async Task<int> AddAsync(OrderForm orderForm)
        {
            var createdOrderForm = await _context.OrderForms.AddAsync(orderForm);
            await _context.SaveChangesAsync();
            return createdOrderForm.Entity.ProcurementId;
        }

        public async Task UpdateSignatureAsync(
            int formId,
            int userId,
            bool isApproved,
            string? memo)
        {
            var signaturesQuery = _context.OrderFormSignatures
                .Where(s => s.FormId == formId);

            var signatures = await signaturesQuery.ToListAsync();

            var headCount = await signaturesQuery.Select(s => s.UserId)
                                            .Distinct()
                                            .CountAsync();

            var domainSignatures = signatures.Select(s =>
                new Signature(
                    s.FormId,
                    s.UserId,
                    s.Role.ToString(),
                    s.Memo)).ToList();

            var approvalService = new ApprovalService(domainSignatures);

            if (isApproved)
            {
                approvalService.ApproveSignature(formId, userId, memo);
                _context.OrderFormSignatures.UpdateRange(signatures);
                await _context.SaveChangesAsync();
            }
            else
            {
                approvalService.RejectSignature(formId, userId, memo);
                _context.OrderFormSignatures.UpdateRange(signatures);
                await _context.SaveChangesAsync();
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

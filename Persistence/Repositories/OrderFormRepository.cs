using Domain.Entities;
using Domain.Entities.Forms;
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
        // create
        public async Task<int> AddAsync(OrderForm orderForm)
        {
            var createdOrderForm = await _context.OrderForms.AddAsync(orderForm);
            await _context.SaveChangesAsync();
            return createdOrderForm.Entity.ProcurementId;
        }

        public async Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers)
        {
            var orderFormSignatures = signatureMembers.Select(signature =>
                                    new OrderFormSignature(
                                        signature.FormId,
                                        signature.UserId,
                                        signature.Role.ToString(),
                                        signature.Memo
                                    )).ToList();

            _context.OrderFormSignatures.AddRange(orderFormSignatures);
            await _context.SaveChangesAsync();
        }

        // update
        public async Task<string> UpdateOrderFormSignatureAsync(int formId, int userId, bool isApproved, string? memo)
        {
            var signatures = await _context.OrderFormSignatures
                                           .Where(s => s.FormId == formId)
                                           .ToListAsync();

            var orderForm = await _context.OrderForms
                              .Include(of => of.Form) 
                              .FirstOrDefaultAsync(of => of.FormId == formId);

            if (orderForm == null)
                throw new InvalidOperationException($"OrderForm with ID {formId} not found.");

            var approvalService = new ApprovalService<OrderFormSignature>(signatures);

            if (isApproved)
            {
                approvalService.ApproveSignature(formId, userId, memo);
            }
            else
            {
                approvalService.RejectSignature(formId, userId, memo);
            }

            var newStatus = approvalService.GetStatus(formId);
            orderForm.UpdateStatus(newStatus);

            await _context.SaveChangesAsync();

            return newStatus.Name;
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
    }
}

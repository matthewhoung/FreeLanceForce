using Application.Forms.DTOs;
using Domain.DTOs;
using Domain.Entities;
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
            //signature details
            var signatures = await GetFromSignaturesAsync(formId);
            //line item details
            var lineItems = await GetLineItemsAsync(formId);
            // base form details
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
                            UpdatedAt = orderform.UpdateAt,
                            LineItems = lineItems,
                            Signatures = signatures
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SignatureDto>> GetFromSignaturesAsync(int formId)
        {
            var query = from OrderFormSignatures in _context.OrderFormSignatures
                        where OrderFormSignatures.FormId == formId
                        select new SignatureDto
                        {
                            FormId = OrderFormSignatures.FormId,
                            UserId = OrderFormSignatures.UserId,
                            Role = OrderFormSignatures.Role.ToString(),
                            Memo = OrderFormSignatures.Memo,
                            IsApproved = OrderFormSignatures.IsApproved,
                            ApprovedAt = OrderFormSignatures.ApprovedAt,
                            IsRejected = OrderFormSignatures.IsRejected,
                            RejectedAt = OrderFormSignatures.RejectedAt
                        };

            return await query.ToListAsync();
        }

        
        public async Task<IEnumerable<LineItem>> GetLineItemsAsync(int formId)
        {
            var query = from lineItem in _context.LineItems
                        where lineItem.FormId == formId
                        select lineItem;

            return await query.ToListAsync();
        }

        public async Task<int> AddBaseFormAsync(Form form)
        {
            var createdForm = await _context.Forms.AddAsync(form);
            await _context.SaveChangesAsync();
            return createdForm.Entity.Id;
        }

        public async Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers)
        {
            var orderFormSignatures = signatureMembers.Select(signature => new OrderFormSignature(
                signature.FormId,
                signature.UserId,
                signature.Role.ToString(),
                signature.Memo
            )).ToList();

            _context.OrderFormSignatures.AddRange(orderFormSignatures);
            await _context.SaveChangesAsync();
        }

        public async Task AddLineItemsAsync(IEnumerable<LineItem> itemDetail)
        {
            var lineItems = itemDetail.Select(item => new LineItem(
                item.FormId,
                item.Title,
                item.Description,
                item.Price,
                item.Quantity
                )).ToList();

            _context.LineItems.AddRange(lineItems);
            await _context.SaveChangesAsync();
        }
    }
}

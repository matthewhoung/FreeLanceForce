using Application.Forms.DTOs;
using Domain.Entities;
using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IGenericFormRepository
    {
        Task<int> AddBaseFormAsync(Form form);
        Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers);
        Task AddLineItemsAsync(IEnumerable<LineItem> lineItems);
        Task<FormDetailDto> GetFormDetailsByIdAsync(int formId);
    }
}

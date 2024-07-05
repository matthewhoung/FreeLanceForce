using Application.Forms.DTOs;
using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IGenericFormRepository
    {
        //create
        Task<int> AddBaseFormAsync(Form form);
        Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers);
        Task AddLineItemsAsync(IEnumerable<LineItem> lineItems);
        //read
        Task<FormDetailDto> GetFormDetailsByIdAsync(int formId);
        Task<IEnumerable<SignatureDto>> GetFromSignaturesAsync(int formId);
        Task<IEnumerable<LineItem>> GetLineItemsAsync(int formId);
    }
}

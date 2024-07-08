using Domain.Entities;
using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {
        // create
        Task<int> AddAsync(OrderForm orderForm);
        Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers);
        // update
        Task UpdateTitleAsync(int formId, string title);// haven't implemented
        Task UpdateDescriptionAsync(int formId, string description);// haven't implemented
        Task<string> UpdateOrderFormSignatureAsync(int formId, int userId, bool isApproved, string? memo);
    }
}

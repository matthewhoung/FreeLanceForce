using Domain.Entities.Forms;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {
        // create
        Task<int> AddAsync(OrderForm orderForm);
        // update
        Task UpdateTitleAsync(int formId, string title);
        Task UpdateDescriptionAsync(int formId, string description);
        Task UpdateOrderFormSignatureAsync(int formId, int userId, bool isApproved, string? memo);
        // delete
        Task DeleteAsync(int formId);
    }
}

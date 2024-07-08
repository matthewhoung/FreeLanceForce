using Domain.Entities.Forms;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {
        Task<int> AddAsync(OrderForm orderForm);
        Task DeleteAsync(int formId);
        Task UpdateStatusAsync(int formId, Status status);
        Task UpdateTitleAsync(int formId, string title);
        Task UpdateDescriptionAsync(int formId, string description);
        Task UpdateOrderFormSignatureAsync(int formId, int userId, bool isApproved, string? memo);
    }
}

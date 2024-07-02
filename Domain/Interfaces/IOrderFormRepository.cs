using Application.Forms.DTOs;
using Domain.Forms;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {
        Task<OrderForm> GetByIdAsync(int formId);
        Task<IEnumerable<OrderForm>> GetAllAsync();
        Task<int> AddAsync(OrderForm orderForm);
        Task UpdateAsync(OrderForm orderForm);
        Task DeleteAsync(int procurementId);

        //testing advanced querying
        Task<OrderFormDto> GetOrderFormDetailsByIdAsync(int formId);
    }
}

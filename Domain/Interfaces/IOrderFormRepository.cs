using Application.Forms.DTOs;
using Domain.Forms;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {

        Task<int> AddAsync(OrderForm orderForm);
        Task UpdateAsync(OrderForm orderForm);
        Task DeleteAsync(int procurementId);
    }
}

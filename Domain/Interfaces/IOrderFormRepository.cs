using Domain.Forms;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {
        Task<OrderForm> GetByIdAsync(int procurementId);
        Task<IEnumerable<OrderForm>> GetAllAsync();
        Task<int> AddAsync(OrderForm orderForm);
        Task UpdateAsync(OrderForm orderForm);
        Task DeleteAsync(int procurementId);
        Task<int> GetSerialNumberCountAsync(string datePart);
    }
}

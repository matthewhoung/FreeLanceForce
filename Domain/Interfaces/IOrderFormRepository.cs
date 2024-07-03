using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IOrderFormRepository
    {

        Task<int> AddAsync(OrderForm orderForm);
        Task UpdateAsync(OrderForm orderForm);
        Task DeleteAsync(int procurementId);
    }
}

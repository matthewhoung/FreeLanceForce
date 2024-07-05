using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IPaymentFormRepository
    {
        Task<int> AddAsync(PaymentForm form);
    }
}

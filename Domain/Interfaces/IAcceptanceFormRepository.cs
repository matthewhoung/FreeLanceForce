using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IAcceptanceFormRepository
    {
        Task<int> AddAsync(AcceptanceForm form);
    }
}

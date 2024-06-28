using Domain.Forms;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        Task<Form> GetByIdAsync(int id);
        Task<IEnumerable<Form>> GetAllAsync();
        Task<int> AddAsync(Form form);
        Task UpdateAsync(Form form);
        Task DeleteAsync(int id);
    }
}

using Application.Forms.DTOs;
using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IGenericFormRepository
    {
        Task<FormDetailDto> GetFormDetailsByIdAsync(int formId);
        Task<int> AddAsync(Form form);
        Task UpdateAsync(Form form);
        Task DeleteAsync(int id);
    }
}

using Application.Forms.DTOs;
using Domain.Entities;
using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IGenericFormRepository
    {
        Task<int> AddBaseFormAsync(Form form);
        Task<FormDetailDto> GetFormDetailsByIdAsync(int formId);
        Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers);
    }
}

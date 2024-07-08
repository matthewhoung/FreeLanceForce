using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAcceptanceFormRepository
    {
        Task AddAsync(int formId);
        Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers);
    }
}

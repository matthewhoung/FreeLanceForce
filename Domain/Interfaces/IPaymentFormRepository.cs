using Domain.Entities;
using Domain.Entities.Forms;

namespace Domain.Interfaces
{
    public interface IPaymentFormRepository
    {
        Task AddAsync(int formId);
        Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers);
    }
}

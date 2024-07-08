using Domain.Entities;
using Domain.Enums;

namespace Domain.Services
{
    public class ApprovalService<T> where T : Signature
    {
        private readonly List<T> _signatures;

        public ApprovalService(List<T> signatures)
        {
            _signatures = signatures;
        }

        public void ApproveSignature(int formId, int userId, string? memo)
        {
            var signature = _signatures.FirstOrDefault(s => s.FormId == formId && s.UserId == userId);

            if (signature == null)
                throw new InvalidOperationException("Signature not found.");

            if (signature.Role == Roles.Director && !IsManagerSigned())
                throw new InvalidOperationException("Manager must sign before the Director.");

            if (signature.Role == Roles.Manager && !AreParticipantsSigned())
                throw new InvalidOperationException("All participants must sign before the Manager.");

            signature.Approve(memo);
        }

        public void RejectSignature(int formId, int userId, string? memo)
        {
            var signature = _signatures.FirstOrDefault(s => s.FormId == formId && s.UserId == userId);

            if (signature == null)
                throw new InvalidOperationException("Signature not found.");

            signature.Reject(memo);
        }

        private bool AreParticipantsSigned()
        {
            return !_signatures.Any(s =>
                                 (s.IsApproved == false || s.IsApproved == null) &&
                                 s.Role != Roles.Manager &&
                                 s.Role != Roles.Director);
        }

        private bool IsManagerSigned()
        {
            var managerSignature = _signatures.FirstOrDefault(s => s.Role == Roles.Manager);
            return managerSignature != null && managerSignature.IsApproved == true;
        }
    }
}

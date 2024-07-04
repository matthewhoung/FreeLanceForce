using Domain.Entities;
using Domain.Enums;

namespace Domain.Services
{
    public class ApprovalService
    {
        private readonly List<Signature> _signatures;

        public ApprovalService(List<Signature> signatures)
        {
            _signatures = signatures;
        }

        public void ApproveSignature(int formId, int userId, DateTime approvedAt)
        {
            var signature = _signatures.FirstOrDefault(s => s.FormId == formId && 
                                                       s.UserId == userId);
            if (signature == null)
                throw new InvalidOperationException("Signature not found.");

            if (signature.Role == Roles.Director)
                EnsureManagerSigned(formId);

            else if (signature.Role == Roles.Manager)
                EnsureParticipantsSigned(formId);

            signature.Approve(approvedAt);
        }

        public void RejectSignature(int formId, int userId, DateTime rejectedAt)
        {
            var signature = _signatures.FirstOrDefault(s => s.FormId == formId && 
                                                       s.UserId == userId);
            if (signature == null)
                throw new InvalidOperationException("Signature not found.");

            signature.Reject(rejectedAt);
        }

        private void EnsureParticipantsSigned(int formId)
        {
            if (_signatures.Any(s => s.FormId == formId && 
                               !s.IsApproved && 
                                s.Role != Roles.Manager && 
                                s.Role != Roles.Director))
            {
                throw new InvalidOperationException("All participants must sign before the Manager.");
            }
        }

        private void EnsureManagerSigned(int formId)
        {
            var managerSignature = _signatures.FirstOrDefault(s => s.FormId == formId && 
                                                                   s.Role == Roles.Manager);
            if (managerSignature == null || !managerSignature.IsApproved)
            {
                throw new InvalidOperationException("Manager must sign before the Director.");
            }
        }
    }
}

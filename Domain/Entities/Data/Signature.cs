using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Signature
    {
        public int SignatureId { get; private set; }
        public int FormId { get; private set; }
        public List<SignatureMember> SignatureMembers { get; private set; } = new List<SignatureMember>();

        private Signature() 
        {
        }

        public Signature(int formId)
        {
            FormId = formId;
        }

        public void AddSignatureMember(SignatureMember signatureMember)
        {
            if (signatureMember == null) 
                throw new ArgumentNullException(nameof(signatureMember));
            SignatureMembers.Add(signatureMember);
        }

        public void Approve(int userId, DateTime approvedAt)
        {
            var member = SignatureMembers.FirstOrDefault(m => m.UserId == userId);

            if (member == null) 
                throw new InvalidOperationException("Signature member not found.");

            if (member.Role == Roles.Director)
            {
                EnsureManagerSigned();
            }
            else if (member.Role == Roles.Manager)
            {
                EnsureParticipantsSigned();
            }

            member.Approve(approvedAt);
        }

        public void Reject(int userId, DateTime rejectedAt)
        {
            var member = SignatureMembers.FirstOrDefault(m => m.UserId == userId);
            if (member == null) 
                throw new InvalidOperationException("Signature member not found.");

            member.Reject(rejectedAt);
        }

        private void EnsureParticipantsSigned()
        {
            if (SignatureMembers.Any(m => !m.IsApproved && 
                                    m.Role != Roles.Manager && 
                                    m.Role != Roles.Director))
            {
                throw new InvalidOperationException("All participants must sign before the Manager.");
            }
        }

        private void EnsureManagerSigned()
        {
            var manager = SignatureMembers.FirstOrDefault(m => m.Role == Roles.Manager);

            if (manager == null || !manager.IsApproved)
            {
                throw new InvalidOperationException("Manager must sign before the Director.");
            }
        }
    }
}

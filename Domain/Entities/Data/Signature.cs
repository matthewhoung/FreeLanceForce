using Domain.Enums;

namespace Domain.Entities
{
    public class Signature
    {
        public int SignatureId { get; private set; }
        public int FormId { get; private set; }
        public int UserId { get; private set; }
        public Roles Role { get; private set; }
        public string? Memo { get; private set; }
        public bool? IsApproved { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public bool? IsRejected { get; private set; }
        public DateTime? RejectedAt { get; private set; }

        protected Signature()
        {
        }

        public Signature(int formId, int userId, string role, string? memo)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId cannot be less than or equal to zero.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Role cannot be empty.", nameof(role));
            }
            FormId = formId;
            UserId = userId;
            Role = Roles.FromName(role);
            Memo = memo;
        }

        public void Approve(DateTime approvedAt,string? memo)
        {
            IsApproved = true;
            ApprovedAt = approvedAt;
            IsRejected = false;
            RejectedAt = null;
            Memo = memo;
        }

        public void Reject(DateTime rejectedAt,string? memo)
        {
            IsApproved = false;
            IsRejected = true;
            RejectedAt = rejectedAt;
            Memo = memo;
        }
    }
}

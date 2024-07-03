using Domain.Enums;

namespace Domain.ValueObjects
{
    public class SignatureMember
    {
        public int UserId { get; private set; }
        public Roles Role { get; private set; }
        public string? Memo { get; private set; }
        public bool IsApproved { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public bool IsRejected { get; private set; }
        public DateTime? RejectedAt { get; private set; }

        public SignatureMember(int userId, string role, string? memo)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId cannot be less than or equal to zero.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Role cannot be empty.", nameof(role));
            }
            UserId = userId;
            Role = Roles.FromName(role);
            Memo = memo;
        }

        public void Approve(DateTime approvedAt)
        {
            IsApproved = true;
            ApprovedAt = approvedAt;
            IsRejected = false;
            RejectedAt = null;
        }

        public void Reject(DateTime rejectedAt)
        {
            IsApproved = false;
            IsRejected = true;
            RejectedAt = rejectedAt;
        }
    }
}

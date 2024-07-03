using MediatR;

namespace Application.Forms.Commands
{
    public class ApproveSignatureCommand : IRequest<Unit>
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public DateTime ApprovedAt { get; set; }

        public ApproveSignatureCommand(int formId, int userId, DateTime approvedAt)
        {
            FormId = formId;
            UserId = userId;
            ApprovedAt = approvedAt;
        }
    }
}

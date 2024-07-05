using MediatR;

namespace Application.Forms.Commands
{
    public class UpdateSignatureCommand : IRequest<Unit>
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public bool IsApproved { get; set; }
        public string? Memo { get; set; }

        public UpdateSignatureCommand(int formId, int userId, bool isApproved, string? memo)
        {
            FormId = formId;
            UserId = userId;
            IsApproved = isApproved;
            Memo = memo;
        }
    }
}

using Application.Forms.Commands;
using Domain.Services;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class ApproveSignatureHandler : IRequestHandler<ApproveSignatureCommand,Unit>
    {
        private readonly ApprovalService _approvalService;

        public ApproveSignatureHandler(ApprovalService approvalService)
        {
            _approvalService = approvalService ?? throw new ArgumentNullException(nameof(approvalService));
        }

        public Task<Unit> Handle(ApproveSignatureCommand request, CancellationToken cancellationToken)
        {
            _approvalService.ApproveSignature(request.FormId, request.UserId, request.ApprovedAt);
            return Task.FromResult(Unit.Value);
        }
    }
}

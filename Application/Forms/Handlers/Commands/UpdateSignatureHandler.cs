using Application.Forms.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class UpdateSignatureHandler : IRequestHandler<UpdateSignatureCommand,Unit>
    {
        private readonly IOrderFormRepository _orderFormRepository;
        private readonly IAcceptanceFormRepository _acceptanceFormRepository;

        public UpdateSignatureHandler(IOrderFormRepository orderFormRepository, IAcceptanceFormRepository acceptanceFormRepository)
        {
            _orderFormRepository = orderFormRepository;
            _acceptanceFormRepository = acceptanceFormRepository;
        }

        public async Task<Unit> Handle(UpdateSignatureCommand request, CancellationToken cancellationToken)
        {
            var currentStatus = await _orderFormRepository.UpdateOrderFormSignatureAsync(request.FormId,request.UserId,request.IsApproved,request.Memo);
            if (currentStatus == "Finished")
            {
                await _acceptanceFormRepository.AddAsync(request.FormId);
            }

            return Unit.Value;
        }
    }
}

using Application.Forms.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class UpdateSignatureHandler : IRequestHandler<UpdateSignatureCommand,Unit>
    {
        private readonly IOrderFormRepository _orderFormRepository;

        public UpdateSignatureHandler(IOrderFormRepository orderFormRepository)
        {
            _orderFormRepository = orderFormRepository;
        }

        public async Task<Unit> Handle(UpdateSignatureCommand request, CancellationToken cancellationToken)
        {
            await _orderFormRepository.UpdateOrderFormSignatureAsync(request.FormId,request.UserId,request.IsApproved,request.Memo);

            return Unit.Value;
        }
    }
}

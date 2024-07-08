using Application.Forms.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateSignatureMemberHandler : IRequestHandler<CreateSignatureMemberCommand, Unit>
    {
        private readonly IOrderFormRepository _orderformRepository;

        public CreateSignatureMemberHandler(IOrderFormRepository orderformRepository)
        {
            _orderformRepository = orderformRepository;
        }

        public async Task<Unit> Handle(CreateSignatureMemberCommand request, CancellationToken cancellationToken)
        {
            var signatures = request.CreateSignatureMember.Select(dto => new Signature(
                dto.FormId,
                dto.UserId,
                dto.Role,
                dto.Memo
            )).ToList();

            await _orderformRepository.AddSignatureMembersAsync(signatures);

            return Unit.Value;
        }
    }
}

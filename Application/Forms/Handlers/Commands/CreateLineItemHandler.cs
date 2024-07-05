using Application.Forms.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateLineItemHandler : IRequestHandler<CreateLineItemCommand, Unit>
    {
        private readonly IGenericFormRepository _genericFormRepository;

        public CreateLineItemHandler(IGenericFormRepository genericFormRepository)
        {
            _genericFormRepository = genericFormRepository;
        }

        public async Task<Unit> Handle(CreateLineItemCommand request, CancellationToken cancellationToken)
        {
            var lineItems = request.LineItems.Select(dto => new LineItem(
                dto.FormId,
                dto.Title,
                dto.Description,
                dto.Price,
                dto.Quantity
            )).ToList();

            await _genericFormRepository.AddLineItemsAsync(lineItems);

            return Unit.Value;
        }
    }
}

using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Commands
{
    public class CreateLineItemCommand : IRequest<Unit>
    {
        public IEnumerable<CreateLineItemDTO> LineItems { get; set; }

        public CreateLineItemCommand(IEnumerable<CreateLineItemDTO> lineItems)
        {
            LineItems = lineItems;
        }
    }
}

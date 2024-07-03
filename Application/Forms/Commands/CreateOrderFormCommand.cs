using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Commands
{
    public class CreateOrderFormCommand : IRequest<int>
    {
        public CreateFormDTO CreateFormDto { get; set; }

        public CreateOrderFormCommand(CreateFormDTO createFormDto)
        {
            CreateFormDto = createFormDto;
        }
    }
}

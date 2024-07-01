using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormCommand : IRequest<int>
    {
        public CreateFormDTO CreateFormDto { get; set; }

        public CreateFormCommand(CreateFormDTO createFormDto)
        {
            CreateFormDto = createFormDto;
        }
    }
}

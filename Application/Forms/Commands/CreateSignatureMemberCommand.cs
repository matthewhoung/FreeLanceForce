using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Commands
{
    public class CreateSignatureMemberCommand : IRequest<Unit>
    {
        public IEnumerable<CreateSignatureMemberDTO> CreateSignatureMember { get; set; }

        public CreateSignatureMemberCommand(IEnumerable<CreateSignatureMemberDTO> createSignatureMemberDTO)
        {
            CreateSignatureMember = createSignatureMemberDTO;
        }
    }
}

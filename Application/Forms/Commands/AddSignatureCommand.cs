using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Commands
{
    public class AddSignatureCommand : IRequest<IEnumerable<int>>
    {
        public IEnumerable<SignatureMemberDTO> Signatures { get; set; }

        public AddSignatureCommand(IEnumerable<SignatureMemberDTO> signatures)
        {
            Signatures = signatures;
        }
    }
}

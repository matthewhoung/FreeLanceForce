using Domain.DTOs;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetSignatureByIdQuery : IRequest<IEnumerable<SignatureDto>>
    {
        public int FormId { get; set; }

        public GetSignatureByIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}

using Application.Forms.Queries;
using Domain.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetSignatureByIdHandler : IRequestHandler<GetSignatureByIdQuery, IEnumerable<SignatureDto>>
    {
        private readonly IGenericFormRepository _formRepository;

        public GetSignatureByIdHandler(IGenericFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<IEnumerable<SignatureDto>> Handle(GetSignatureByIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFromSignaturesAsync(request.FormId);
        }
    }
}

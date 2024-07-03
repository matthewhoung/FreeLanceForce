using Application.Forms.DTOs;
using Application.Forms.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormByIdHandler : IRequestHandler<GetFormByIdQuery, FormDetailDto>
    {
        private readonly IGenericFormRepository _formRepository;

        public GetFormByIdHandler(IGenericFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<FormDetailDto> Handle(GetFormByIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetFormDetailsByIdAsync(request.FormId);
        }
    }
}

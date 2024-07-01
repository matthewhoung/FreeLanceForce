using Application.Forms.Queries;
using Domain.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetFormByIdHandler : IRequestHandler<GetFormByIdQuery, Form>
    {
        private readonly IFormRepository _formRepository;

        public GetFormByIdHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<Form> Handle(GetFormByIdQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetByIdAsync(request.Id);
        }
    }
}

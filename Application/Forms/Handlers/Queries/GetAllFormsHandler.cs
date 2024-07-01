using Application.Forms.Queries;
using Domain.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetAllFormsHandler : IRequestHandler<GetAllFormsQuery, IEnumerable<Form>>
    {
        private readonly IFormRepository _formRepository;

        public GetAllFormsHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<IEnumerable<Form>> Handle(GetAllFormsQuery request, CancellationToken cancellationToken)
        {
            return await _formRepository.GetAllAsync();
        }
    }
    
}

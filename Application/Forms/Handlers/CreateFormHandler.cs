using Application.Forms.Commands;
using Domain.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers
{
    public class CreateFormHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly IFormRepository _formRepository;

        public CreateFormHandler(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var form = new Form(
                    productId: request.ProjectId,
                    title: request.Title,
                    description: request.Description,
                    stage: request.Stage
                );

                var createdFormId = await _formRepository.AddAsync(form);
                return createdFormId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new InvalidOperationException("An error occurred while creating a new form", ex);
            }
        }
    }
}

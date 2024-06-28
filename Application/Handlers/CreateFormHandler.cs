using Application.Commands;
using Domain.Forms;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
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
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occured while creating new form",ex);
            }
        }
    }
}

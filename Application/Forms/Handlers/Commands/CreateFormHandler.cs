using Application.Forms.Commands;
using Domain.Forms;
using Domain.Forms.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly IFormRepository _formRepository;
        private readonly IOrderFormRepository _orderFormRepository;

        public CreateFormHandler(IFormRepository formRepository, IOrderFormRepository orderFormRepository)
        {
            _formRepository = formRepository;
            _orderFormRepository = orderFormRepository;
        }

        public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.CreateFormDto;

                // Create the base form
                var form = new Form(
                    productId: dto.ProjectId,
                    stage: dto.Stage ?? "OrderForm"
                );

                var createdFormId = await _formRepository.AddAsync(form);

                var serialNumber = await _orderFormRepository.GenerateSerialNumberAsync(
                    formId: createdFormId,
                    stage: "OrderForm",
                    isAttatchForm: null);

                //Create the order form
                var status = Status.FromName(dto.Status ?? "Pending");
                var orderForm = new OrderForm(
                    formId: createdFormId,
                    serialNumber: serialNumber,
                    title: dto.Title,
                    description: dto.Description,
                    status: status
                );

                await _orderFormRepository.AddAsync(orderForm);

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

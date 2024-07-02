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
        private readonly ISerialNumberRepository _serialNumberRepository;

        public CreateFormHandler(IFormRepository formRepository, IOrderFormRepository orderFormRepository,ISerialNumberRepository serialNumberRepository)
        {
            _formRepository = formRepository;
            _orderFormRepository = orderFormRepository;
            _serialNumberRepository = serialNumberRepository;

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

                //取得採購單編號                
                var serialNumber = await _serialNumberRepository.SerialNumberAsync(
                    formId: dto.FormId?? createdFormId,
                    stage: dto.Stage?? "OrderForm",
                    isAttach: dto.IsAttach?? null);

                //創建採購單
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

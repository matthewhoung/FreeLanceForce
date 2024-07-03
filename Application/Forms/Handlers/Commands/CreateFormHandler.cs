using Application.Forms.Commands;
using Domain.Entities.Forms;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateFormHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly IGenericFormRepository _formRepository;
        private readonly IOrderFormRepository _orderFormRepository;
        private readonly ISerialNumberRepository _serialNumberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFormHandler(
            IGenericFormRepository formRepository,
            IOrderFormRepository orderFormRepository,
            ISerialNumberRepository serialNumberRepository,
            IUnitOfWork unitOfWork)
        {
            _formRepository = formRepository;
            _orderFormRepository = orderFormRepository;
            _serialNumberRepository = serialNumberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
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
                var serialNumber = await _serialNumberRepository.GenerateSerialNumberAsync(
                    formId: dto.FormId ?? createdFormId,//唯有追加追減建構時才需要填入
                    stage: dto.Stage ?? "OrderForm",
                    isAttach: dto.IsAttach ?? null);//true:追加單, false:追減單

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

                await _unitOfWork.CommitAsync();
                return createdFormId;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                Console.WriteLine($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new InvalidOperationException("An error occurred while creating a new form", ex);
            }
        }
    }
}

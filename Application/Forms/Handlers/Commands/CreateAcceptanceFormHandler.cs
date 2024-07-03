using Application.Forms.Commands;
using Domain.Entities.Forms;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class CreateAcceptanceFormHandler : IRequestHandler<CreateAcceptanceFormCommand, Unit>
    {
        private readonly IAcceptanceFormRepository _acceptanceFormRepository;
        private readonly IGenericFormRepository _formRepository;
        private readonly ISerialNumberRepository _serialNumberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAcceptanceFormHandler(
            IAcceptanceFormRepository acceptanceFormRepository,
            IGenericFormRepository formRepository,
            ISerialNumberRepository serialNumberRepository,
            IUnitOfWork unitOfWork)
        {
            _acceptanceFormRepository = acceptanceFormRepository;
            _formRepository = formRepository;
            _serialNumberRepository = serialNumberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateAcceptanceFormCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var inheritForm = await _formRepository.GetFormDetailsByIdAsync(request.FormId);
                if (inheritForm == null)
                {
                    throw new InvalidOperationException($"Form with Id {request.FormId} not found.");
                }

                var newSerialNumber = await _serialNumberRepository.GenerateSerialNumberAsync(
                    formId: request.FormId,
                    stage: "AcceptanceForm",
                    isAttach: null);

                var acceptanceForm = new AcceptanceForm(
                    formId: request.FormId,
                    serialNumber: newSerialNumber,
                    title: inheritForm.Title,
                    description: null,
                    status: Status.FromName("Pending")
                );

                await _acceptanceFormRepository.AddAsync(acceptanceForm);
                await _unitOfWork.CommitAsync();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                Console.WriteLine($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new InvalidOperationException("Create AcceptanceForm failed", ex);
            }
        }
    }
}

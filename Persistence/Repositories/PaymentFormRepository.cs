using Domain.Entities;
using Domain.Entities.Forms;
using Domain.Enums;
using Domain.Interfaces;

namespace Persistence.Repositories
{
    public class PaymentFormRepository : IPaymentFormRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericFormRepository _genericFormRepository;
        private readonly ISerialNumberRepository _serialNumberRepository;

        public PaymentFormRepository(ApplicationDbContext context, IUnitOfWork unitOfWork, IGenericFormRepository genericFormRepository, ISerialNumberRepository serialNumberRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _genericFormRepository = genericFormRepository;
            _serialNumberRepository = serialNumberRepository;
        }

        public async Task AddAsync(int formId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                //取得繼承表單資訊
                var inheritedForm = await _genericFormRepository.GetFormDetailsByIdAsync(formId);
                if (inheritedForm == null)
                {
                    throw new InvalidOperationException($"Form with Id {formId} not found.");
                }

                //取得付款單編號
                var newSerialNumber = await _serialNumberRepository.GenerateSerialNumberAsync(
                    formId: formId,
                    stage: "PaymentForm",
                    isAttach: null
                    );

                //創建付款單
                var paymentForm = new PaymentForm(
                    formId: formId,
                    serialNumber: newSerialNumber,
                    title: inheritedForm.Title,
                    description: null,
                    status: Status.FromName("Pending")
                    );

                //創建簽核人員名單
                var signatures = inheritedForm.Signatures.Select(signature =>
                                 new PaymentFormSignature(
                                     formId: formId,
                                     userId: signature.UserId,
                                     role: signature.Role.ToString(),
                                     memo: null
                                     )).ToList();

                _context.PaymentForms.Add(paymentForm);
                await AddSignatureMembersAsync(signatures);
                await _context.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                Console.WriteLine($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}");

                throw new Exception(ex.Message);
            }
        }

        public async Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers)
        {
            var paymentFormSignatures = signatureMembers.Select(signature =>
                                        new PaymentFormSignature(
                                            formId: signature.FormId,
                                            userId: signature.UserId,
                                            role: signature.Role.ToString(),
                                            memo: null
                                            )).ToList();

            _context.PaymentFormSignatures.AddRange(paymentFormSignatures);
            await _context.SaveChangesAsync();
        }
    }
}

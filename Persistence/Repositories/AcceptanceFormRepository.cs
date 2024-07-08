using Domain.Entities;
using Domain.Entities.Forms;
using Domain.Enums;
using Domain.Interfaces;

namespace Persistence.Repositories
{
    public class AcceptanceFormRepository : IAcceptanceFormRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericFormRepository _genericFormRepository;
        private readonly ISerialNumberRepository _serialNumberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AcceptanceFormRepository(ApplicationDbContext context, IUnitOfWork unitOfWork, IGenericFormRepository genericFormRepository, ISerialNumberRepository serialNumberRepository)
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

                //取得驗收單編號
                var newSerialNumber = await _serialNumberRepository.GenerateSerialNumberAsync(
                    formId: formId,
                    stage: "AcceptanceForm",
                    isAttach: null
                    );

                //創建驗收單
                var acceptanceForm = new AcceptanceForm(
                    formId: formId,
                    serialNumber: newSerialNumber,
                    title: inheritedForm.Title,
                    description: null,
                    status: Status.FromName("Pending")
                    );

                //創建驗收人員名單
                var signatures = inheritedForm.Signatures.Select(signature =>
                                 new AcceptanceFormSignature(
                                     formId: formId,
                                     userId: signature.UserId,
                                     role: signature.Role.ToString(),
                                     memo: null
                                     )).ToList();

                _context.AcceptanceForms.Add(acceptanceForm);
                await AddSignatureMembersAsync(signatures);
                await _context.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                Console.WriteLine($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new InvalidOperationException("Create AcceptanceForm failed", ex);
            }
        }

        public async Task AddSignatureMembersAsync(IEnumerable<Signature> signatureMembers)
        {
            var acceptanceFormSignatures = signatureMembers.Select(signature =>
                                        new AcceptanceFormSignature(
                                            signature.FormId,
                                            signature.UserId,
                                            signature.Role.ToString(),
                                            signature.Memo
                                            )).ToList();

            _context.AcceptanceFormSignatures.AddRange(acceptanceFormSignatures);
            await _context.SaveChangesAsync();
        }
    }
}

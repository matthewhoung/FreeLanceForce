using Application.Forms.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Commands
{
    public class AddSignatureHandler : IRequestHandler<AddSignatureCommand, IEnumerable<int>>
    {
        private readonly IGenericFormRepository _formRepository;

        public AddSignatureHandler(IGenericFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public async Task<IEnumerable<int>> Handle(AddSignatureCommand request, CancellationToken cancellationToken)
        {
            var signatures = request.Signatures.Select(s => new Signature(
                s.FormId,
                s.UserId,
                s.Role,
                s.Memo
            )).ToList();

            return await _formRepository.AddSignatureMembersAsync(signatures);
        }
    }
}

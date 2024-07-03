using MediatR;

namespace Application.Forms.Commands
{
    public class CreateAcceptanceFormCommand : IRequest<Unit>
    {
        public int FormId { get; set; }

        public CreateAcceptanceFormCommand(int formId)
        {
            FormId = formId;
        }
    }
}

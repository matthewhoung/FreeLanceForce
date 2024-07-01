using Domain.Forms;
using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public FormStage? Stage { get; set; }
    }
}

using MediatR;

namespace Application.Forms.Commands
{
    public class CreateFormCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public string? Stage { get; set; }
    }
}

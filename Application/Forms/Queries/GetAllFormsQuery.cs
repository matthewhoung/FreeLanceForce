using Domain.Forms;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetAllFormsQuery : IRequest<IEnumerable<Form>>
    {
    }
}

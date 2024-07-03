using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetFormByIdQuery : IRequest<FormDetailDto>
    {
        public int FormId { get; set; }

        public GetFormByIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}

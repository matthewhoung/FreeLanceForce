using Application.Forms.DTOs;
using MediatR;

namespace Application.Forms.Queries
{
    public class GetOrderFormByIdQuery : IRequest<OrderFormDto>
    {
        public int FormId { get; set; }

        public GetOrderFormByIdQuery(int formId)
        {
            FormId = formId;
        }
    }
}

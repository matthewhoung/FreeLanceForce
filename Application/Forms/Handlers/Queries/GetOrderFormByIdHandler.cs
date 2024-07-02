using Application.Forms.DTOs;
using Application.Forms.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Forms.Handlers.Queries
{
    public class GetOrderFormByIdHandler : IRequestHandler<GetOrderFormByIdQuery, OrderFormDto>
    {
        private readonly IOrderFormRepository _orderFormRepository;

        public GetOrderFormByIdHandler(IOrderFormRepository orderFormRepository)
        {
            _orderFormRepository = orderFormRepository;
        }

        public async Task<OrderFormDto> Handle(GetOrderFormByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderFormRepository.GetOrderFormDetailsByIdAsync(request.FormId);
        }
    }
}

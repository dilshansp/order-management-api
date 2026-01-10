using AutoMapper;
using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;

namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler
        : IRequestHandler<GetOrderByIdQuery, BaseResponse<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return new BaseResponse<OrderDto>(false, "Order not found");

            var dto = _mapper.Map<OrderDto>(order);

            return new BaseResponse<OrderDto>(true, "Order fetched successfully", dto);
        }
    }
}

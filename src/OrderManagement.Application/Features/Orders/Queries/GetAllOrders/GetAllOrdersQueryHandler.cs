using AutoMapper;
using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;

namespace OrderManagement.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler
        : IRequestHandler<GetAllOrdersQuery, BaseResponse<List<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            var dto = _mapper.Map<List<OrderDto>>(orders);

            return new BaseResponse<List<OrderDto>>(true, "Orders retrieved successfully", dto);
        }
    }
}

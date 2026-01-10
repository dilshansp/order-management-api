using AutoMapper;
using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler
        : IRequestHandler<UpdateOrderCommand, BaseResponse<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return new BaseResponse<OrderDto>(false, "Order not found");

            // Update root fields
            order.CustomerName = request.CustomerName;

            // Clear existing items and rebuild
            order.Items.Clear();

            foreach (var item in request.Items)
            {
                order.Items.Add(new OrderItem
                {
                    Id = item.Id ?? Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            await _orderRepository.UpdateAsync(order);

            var dto = _mapper.Map<OrderDto>(order);

            return new BaseResponse<OrderDto>(true, "Order updated successfully", dto);
        }
    }
}

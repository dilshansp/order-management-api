using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Interfaces;

namespace OrderManagement.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler
        : IRequestHandler<DeleteOrderCommand, BaseResponse<string>>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<BaseResponse<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return new BaseResponse<string>(false, "Order not found");

            await _orderRepository.DeleteAsync(request.Id);

            return new BaseResponse<string>(true, "Order deleted successfully");
        }
    }
}

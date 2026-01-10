using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Models;

namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<BaseResponse<OrderDto>>
    {
        public string CustomerName { get; set; } = string.Empty;

        public List<CreateOrderItemModel> Items { get; set; } = new();
    }

    public class CreateOrderItemModel
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

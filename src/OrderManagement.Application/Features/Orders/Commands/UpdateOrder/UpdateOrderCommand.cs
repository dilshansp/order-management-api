using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Models;

namespace OrderManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<BaseResponse<OrderDto>>
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public List<UpdateOrderItemModel> Items { get; set; } = new();
    }

    public class UpdateOrderItemModel
    {
        public Guid? Id { get; set; }   // For existing items
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Models;

namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<BaseResponse<OrderDto>>
    {
        public Guid Id { get; set; }
    }
}

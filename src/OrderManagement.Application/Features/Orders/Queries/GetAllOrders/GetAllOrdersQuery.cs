using MediatR;
using OrderManagement.Application.Common;
using OrderManagement.Application.Models;

namespace OrderManagement.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<BaseResponse<List<OrderDto>>>
    {
    }
}

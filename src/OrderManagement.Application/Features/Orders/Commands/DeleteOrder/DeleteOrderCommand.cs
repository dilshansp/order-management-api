using MediatR;
using OrderManagement.Application.Common;

namespace OrderManagement.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<BaseResponse<string>>
    {
        public Guid Id { get; set; }
    }
}

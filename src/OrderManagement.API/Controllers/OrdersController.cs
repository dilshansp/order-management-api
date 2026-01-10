using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Common;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Application.Features.Orders.Commands.DeleteOrder;
using OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
using OrderManagement.Application.Features.Orders.Queries.GetAllOrders;
using OrderManagement.Application.Features.Orders.Queries.GetOrderById;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ============================
        // GET: api/orders
        // ============================
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(response);
        }

        // ============================
        // GET: api/orders/{id}
        // ============================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetOrderByIdQuery { Id = id });

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // ============================
        // POST: api/orders
        // ============================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response);
        }

        // ============================
        // PUT: api/orders/{id}
        // ============================
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;

            var response = await _mediator.Send(command);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // ============================
        // DELETE: api/orders/{id}
        // ============================
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteOrderCommand { Id = id });

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}

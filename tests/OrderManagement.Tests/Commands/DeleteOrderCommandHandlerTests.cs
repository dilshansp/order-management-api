using FluentAssertions;
using Moq;
using OrderManagement.Application.Features.Orders.Commands.DeleteOrder;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using Xunit;

namespace OrderManagement.Tests.Commands
{
    public class DeleteOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteOrder_WhenFound()
        {
            // Arrange
            var repoMock = new Mock<IOrderRepository>();
            var orderId = Guid.NewGuid();

            repoMock.Setup(r => r.GetByIdAsync(orderId))
                    .ReturnsAsync(new Order { Id = orderId });

            var handler = new DeleteOrderCommandHandler(repoMock.Object);

            // Act
            var result = await handler.Handle(new DeleteOrderCommand { Id = orderId }, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Order deleted successfully");

            repoMock.Verify(r => r.DeleteAsync(orderId), Times.Once);
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Moq;
using OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Entities;
using Xunit;
using OrderManagement.Application;

namespace OrderManagement.Tests.Commands
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _repoMock;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandlerTests()
        {
            _repoMock = new Mock<IOrderRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldUpdateOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            var existingOrder = new Order
            {
                Id = orderId,
                CustomerName = "Old",
                Items = new List<OrderItem>()
            };

            var command = new UpdateOrderCommand
            {
                Id = orderId,
                CustomerName = "New",
                Items = new List<UpdateOrderItemModel>
                {
                    new UpdateOrderItemModel
                    {
                        ProductName = "Updated Product",
                        Quantity = 2,
                        UnitPrice = 20
                    }
                }
            };

            _repoMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(existingOrder);

            var handler = new UpdateOrderCommandHandler(_repoMock.Object, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
            response.Data.CustomerName.Should().Be("New");
            response.Data.Items.Should().HaveCount(1);

            _repoMock.Verify(r => r.UpdateAsync(existingOrder), Times.Once);
        }
    }
}

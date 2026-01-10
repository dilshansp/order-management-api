using AutoMapper;
using Moq;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Entities;
using Xunit;
using FluentAssertions;
using OrderManagement.Application;

namespace OrderManagement.Tests.Commands
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldCreateOrder_WhenValidRequest()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerName = "Test User",
                Items = new List<CreateOrderItemModel>
                {
                    new CreateOrderItemModel { ProductName = "Item A", Quantity = 1, UnitPrice = 10 }
                }
            };

            var savedOrder = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = command.CustomerName,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "Item A",
                        Quantity = 1,
                        UnitPrice = 10
                    }
                }
            };

            _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
                                .ReturnsAsync(savedOrder);

            var handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.CustomerName.Should().Be("Test User");
            response.Data.Items.Should().HaveCount(1);

            _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}

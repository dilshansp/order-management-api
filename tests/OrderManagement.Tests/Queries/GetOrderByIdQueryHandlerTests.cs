using AutoMapper;
using FluentAssertions;
using Moq;
using OrderManagement.Application.Features.Orders.Queries.GetOrderById;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Entities;
using Xunit;
using OrderManagement.Application;

namespace OrderManagement.Tests.Queries
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _repoMock;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandlerTests()
        {
            _repoMock = new Mock<IOrderRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var id = Guid.NewGuid();

            var order = new Order
            {
                Id = id,
                CustomerName = "John",
                Items = new List<OrderItem>()
            };

            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(order);

            var handler = new GetOrderByIdQueryHandler(_repoMock.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetOrderByIdQuery { Id = id }, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.CustomerName.Should().Be("John");
        }
    }
}

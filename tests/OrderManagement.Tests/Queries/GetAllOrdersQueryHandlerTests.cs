using AutoMapper;
using FluentAssertions;
using Moq;
using OrderManagement.Application.Features.Orders.Queries.GetAllOrders;
using OrderManagement.Application.Interfaces;
using OrderManagement.Application.Models;
using OrderManagement.Domain.Entities;
using Xunit;
using OrderManagement.Application;

namespace OrderManagement.Tests.Queries
{
    public class GetAllOrdersQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllOrders()
        {
            // Arrange
            var repoMock = new Mock<IOrderRepository>();

            repoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Order>
                {
                    new Order { Id = Guid.NewGuid(), CustomerName = "A" },
                    new Order { Id = Guid.NewGuid(), CustomerName = "B" }
                });

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            var handler = new GetAllOrdersQueryHandler(repoMock.Object, mapper);

            // Act
            var result = await handler.Handle(new GetAllOrdersQuery(), CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
        }
    }
}

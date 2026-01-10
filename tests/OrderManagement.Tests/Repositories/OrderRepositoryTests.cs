using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Persistence.Repositories;
using Xunit;

namespace OrderManagement.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // isolated DB per test
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddOrderToDatabase()
        {
            var context = GetDbContext();
            var repo = new OrderRepository(context);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerName = "John",
                Items = new List<OrderItem>()
            };

            await repo.AddAsync(order);

            context.Orders.Count().Should().Be(1);
        }
    }
}

using FluentValidation;

namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MinimumLength(3);

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductName)
                    .NotEmpty().WithMessage("Product name is required.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be > 0");

                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Price must be > 0");
            });
        }
    }
}

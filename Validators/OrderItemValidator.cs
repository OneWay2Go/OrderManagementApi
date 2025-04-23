using FluentValidation;
using OrderManagementApi.Dtos.Order;

namespace OrderManagementApi.Validators
{
    public class OrderItemValidator : AbstractValidator<CreateOrderItemRequestDto>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("Unit price is required")
                .GreaterThan(0).WithMessage("Unit price must be greater than zero");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required")
                .GreaterThan(0).WithMessage("Product ID must be greater than zero");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");
        }
    }
}

using FluentValidation;
using OrderManagementApi.Dtos.Product;

namespace OrderManagementApi.Validators
{
    public class ProductValidator : AbstractValidator<CreateProductRequestDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .Length(1, 100).WithMessage("Name length should be between 1 and 100");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Product price is required")
                .GreaterThan(0).WithMessage("Product price must be greater than zero");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("Product stock is required")
                .GreaterThan(0).WithMessage("Product stock must be greater than zero");
        }
    }
}

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        { 
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale ID is required.");
            RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Sale date is required.");  
            RuleFor(x => x.Items).NotEmpty().WithMessage("Order must contain at least one item.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than zero.");
            RuleFor(x => x.SaleDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.Product).NotEmpty().WithMessage("Product ID is required.");
                items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
                items.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
            });
        }
    }
}

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;

/// <summary>
/// Validator for DeleteOrderCommand
/// </summary>
public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteOrderCommand
    /// </summary>
    public DeleteOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required");
    }
}

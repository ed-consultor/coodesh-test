using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;

/// <summary>
/// Validator for DeleteOrderRequest
/// </summary>
public class DeleteOrderRequestValidator : AbstractValidator<DeleteOrderRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteOrderRequest
    /// </summary>
    public DeleteOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required");
    }
}

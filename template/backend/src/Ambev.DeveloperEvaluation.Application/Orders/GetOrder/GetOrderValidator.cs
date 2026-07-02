using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Validator for GetOrderCommand
/// </summary>
public class GetOrderValidator : AbstractValidator<GetOrderCommand>
{
    /// <summary>
    /// Initializes validation rules for GetOrderCommand
    /// </summary>
    public GetOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required");
    }
}

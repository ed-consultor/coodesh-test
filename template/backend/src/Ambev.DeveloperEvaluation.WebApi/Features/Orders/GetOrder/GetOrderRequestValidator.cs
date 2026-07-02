using FluentValidation;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetOrder;

public class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
{
    public GetOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("order ID is required");
    }
}

using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Command for creating a new order.
/// </summary>
/// <remarks>
/// This command captures the required data to create an order, including the customer identifier,
/// items, total amount, order date and status. It implements <see cref="IRequest{TResponse}"/>
/// to initiate a request that returns a <see cref="CreateOrderResult"/>.
/// 
/// Validation is performed by <see cref="CreateOrderCommandValidator"/> which ensures the fields
/// are correctly populated and follow the required rules.
/// </remarks>
public class CreateOrderCommand : IRequest<CreateOrderResult>
{
    /// <summary>
    /// Gets or sets the identifier of the customer placing the order.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of items included in the order.
    /// </summary>
    public IEnumerable<CreateOrderItem> Items { get; set; } = Enumerable.Empty<CreateOrderItem>();

    /// <summary>
    /// Gets or sets the total amount for the order.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was placed.
    /// </summary>
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the branch associated with the order.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer associated with the order.
    /// </summary>
    public string Customer { get; set; } = string.Empty;   


    public ValidationResultDetail Validate()
    {
        var validator = new CreateOrderCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

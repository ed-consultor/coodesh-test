namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;

/// <summary>
/// Request model for deleting an order
/// </summary>
public class DeleteOrderRequest
{
    /// <summary>
    /// The unique identifier of the order to delete
    /// </summary>
    public Guid Id { get; set; }
}

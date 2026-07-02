namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

/// <summary>
/// Represents the response returned after successfully updated a new order.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly updated order,
/// which can be used for subsequent operations or reference.
/// </remarks>
/// 
public class UpdateOrderResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly updated order.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated order in the system.</value>
    public Guid Id { get; set; }
}
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;

/// <summary>
/// Command for deleting an order
/// </summary>
public record DeleteOrderCommand : IRequest<DeleteOrderResponse>
{
    /// <summary>
    /// The unique identifier of the order to delete
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of DeleteOrderCommand
    /// </summary>
    /// <param name="id">The ID of the order to delete</param>
    public DeleteOrderCommand(Guid id)
    {
        Id = id;
    }
}

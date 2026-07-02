using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Command for retrieving a order by their ID
/// </summary>
public record GetOrderCommand : IRequest<GetOrderResult>
{
    /// <summary>
    /// The unique identifier of the order to retrieve
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of GetOrderCommand
    /// </summary>
    /// <param name="id">The ID of the order to retrieve</param>
    public GetOrderCommand(Guid id)
    {
        Id = id;
    }
}

using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;

/// <summary>
/// Handler for processing DeleteOrderCommand requests
/// </summary>
public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Initializes a new instance of DeleteOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="validator">The validator for DeleteOrderCommand</param>
    public DeleteOrderHandler(
        IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handles the DeleteOrderCommand request
    /// </summary>
    /// <param name="request">The DeleteOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _orderRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Order with ID {request.Id} not found");

        return new DeleteOrderResponse { Success = true };
    }
}

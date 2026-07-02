using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Handler for processing GetOrderCommand requests
/// </summary>
public class GetOrderHandler : IRequestHandler<GetOrderCommand, GetOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetOrderCommand</param>
    public GetOrderHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetOrderCommand request
    /// </summary>
    /// <param name="request">The GetOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order details if found</returns>
    public async Task<GetOrderResult> Handle(GetOrderCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Order with ID {request.Id} not found");

        return _mapper.Map<GetOrderResult>(order);
    }
}

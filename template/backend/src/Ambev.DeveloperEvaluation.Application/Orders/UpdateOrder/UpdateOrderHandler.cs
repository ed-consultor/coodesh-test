using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

/// <summary>
/// Handler for processing UpdateOrderCommand requests
/// </summary>
public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UpdateOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="discountService">The discount service</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateOrderHandler(IOrderRepository orderRepository, IDiscountService discountService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateOrderCommand request
    /// </summary>
    /// <param name="command">The UpdateOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated order details</returns>
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingOrder = await _orderRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingOrder == null)
            throw new InvalidOperationException("Order not found.");

        _mapper.Map(command, existingOrder);

        existingOrder.Items.ForEach(item =>
        {
            var discount = _discountService.Calculate(item.Quantity, item.UnitPrice);
            item.Discount = discount.DiscountAmount;
        });

        var updatedOrder = await _orderRepository.UpdateAsync(existingOrder, cancellationToken);
        var result = _mapper.Map<UpdateOrderResult>(updatedOrder);

        return result;
    }
}

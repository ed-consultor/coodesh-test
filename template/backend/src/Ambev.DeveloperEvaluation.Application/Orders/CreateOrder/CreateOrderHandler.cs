using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Handler for processing CreateOrderCommand requests
/// </summary>
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="discountService">The discount service</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateOrderHandler(IOrderRepository orderRepository, IDiscountService discountService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _discountService = discountService;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateOrderCommand request
    /// </summary>
    /// <param name="command">The CreateOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order details</returns>
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingOrder = await _orderRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingOrder != null)
            throw new InvalidOperationException("An order with the same customer ID already exists.");

        var order = _mapper.Map<Order>(command);

        order.Items.ForEach(item =>
        {
            var discount = _discountService.Calculate(item.Quantity, item.UnitPrice);
            item.Discount = discount.DiscountAmount;
        });

        var createdOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        var result = _mapper.Map<CreateOrderResult>(createdOrder);

        return result;
    }
}

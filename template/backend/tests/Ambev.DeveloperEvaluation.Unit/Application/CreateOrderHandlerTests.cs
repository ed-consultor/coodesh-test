using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.ServiceOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly CreateOrderHandler _handler;

    public CreateOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _discountService = Substitute.For<IDiscountService>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateOrderHandler(_orderRepository, _discountService, _mapper);
    }

    [Fact(DisplayName = "Given valid command When handling Then returns created result")]
    public async Task Handle_ValidRequest_ReturnsCreatedResult()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder(itemsCount: 2);
        var command = new CreateOrderCommand
        {
            SaleNumber = order.SaleNumber,
            Items = order.Items.Select(i => new CreateOrderItem
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalItemAmount = i.TotalItemAmount
            }),
            TotalAmount = order.TotalAmount,
            SaleDate = order.SaleDate,
            Branch = order.Branch,
            Customer = order.Customer
        };

        _orderRepository.GetBySaleNumberAsync(order.SaleNumber, Arg.Any<CancellationToken>())
            .Returns((Order?)null);

        _mapper.Map<Order>(command).Returns(order);

        _discountService.Calculate(Arg.Any<decimal>(), Arg.Any<decimal>())
            .Returns(new DiscountResult { DiscountAmount = 0m, Quantity = 1, UnitPrice = 1, TotalBeforeDiscount = 1, DiscountPercent = 0, TotalAfterDiscount = 1 });

        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>()).Returns(order);

        var expectedResult = new CreateOrderResult { Id = order.Id };
        _mapper.Map<CreateOrderResult>(order).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(order.Id);
        await _orderRepository.Received(1).CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
        _discountService.Received(order.Items.Count).Calculate(Arg.Any<decimal>(), Arg.Any<decimal>());
    }

    [Fact(DisplayName = "Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateOrderCommand(); // empty will fail validation

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}

using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
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

public class UpdateOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly UpdateOrderHandler _handler;

    public UpdateOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _discountService = Substitute.For<IDiscountService>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateOrderHandler(_orderRepository, _discountService, _mapper);
    }

    private UpdateOrderCommand ConvertEntityToCommand(Order order)
    {
        return new UpdateOrderCommand
        {
            SaleNumber = order.SaleNumber,
            SaleDate = order.SaleDate,
            Customer = order.Customer,
            TotalAmount = order.TotalAmount,
            Branch = order.Branch,
            Items = order.Items.Select(i => new UpdateOrderItem
            {
                Product = i.Product,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };
    }

    [Fact(DisplayName = "Given valid command When handling Then returns updated result")]
    public async Task Handle_ValidRequest_ReturnsResult()
    {
        var existing = OrderTestData.GenerateValidOrder(itemsCount: 2);
        var command = new UpdateOrderCommand { SaleNumber = existing.SaleNumber };

        _orderRepository.GetBySaleNumberAsync(existing.SaleNumber, Arg.Any<CancellationToken>()).Returns(existing);

        _discountService.Calculate(Arg.Any<decimal>(), Arg.Any<decimal>())
            .Returns(new DiscountResult { DiscountAmount = 0m });

        command = ConvertEntityToCommand(existing);
        _orderRepository.UpdateAsync(existing, Arg.Any<CancellationToken>()).Returns(existing);
        _mapper.Map<UpdateOrderResult>(existing).Returns(new UpdateOrderResult { Id = existing.Id });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(existing.Id);
        _discountService.Received(existing.Items.Count).Calculate(Arg.Any<decimal>(), Arg.Any<decimal>());
    }

    [Fact(DisplayName = "Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new UpdateOrderCommand();
        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}

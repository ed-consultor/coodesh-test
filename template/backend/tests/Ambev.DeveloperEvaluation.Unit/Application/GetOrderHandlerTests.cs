using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly GetOrderHandler _handler;

    public GetOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetOrderHandler(_orderRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid id When handling Then returns order result")]
    public async Task Handle_ValidRequest_ReturnsResult()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        var command = new GetOrderCommand(order.Id);

        _orderRepository.GetByIdAsync(order.Id, Arg.Any<CancellationToken>()).Returns(order);
        var expected = new GetOrderResult { Id = order.Id };
        _mapper.Map<GetOrderResult>(order).Returns(expected);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(order.Id);
    }

    [Fact(DisplayName = "Given not found id When handling Then throws key not found")]
    public async Task Handle_NotFound_ThrowsKeyNotFoundException()
    {
        var id = Guid.NewGuid();
        var command = new GetOrderCommand(id);
        _orderRepository.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns((Order?)null);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}

using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly DeleteOrderHandler _handler;

    public DeleteOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _handler = new DeleteOrderHandler(_orderRepository);
    }

    [Fact(DisplayName = "Given existing id When deleting Then returns success")]
    public async Task Handle_ExistingId_ReturnsSuccess()
    {
        var id = Guid.NewGuid();
        var command = new DeleteOrderCommand(id);

        _orderRepository.DeleteAsync(id, Arg.Any<CancellationToken>()).Returns(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
    }

    [Fact(DisplayName = "Given non existing id When deleting Then throws key not found")]
    public async Task Handle_NonExistingId_ThrowsKeyNotFound()
    {
        var id = Guid.NewGuid();
        var command = new DeleteOrderCommand(id);

        _orderRepository.DeleteAsync(id, Arg.Any<CancellationToken>()).Returns(false);

        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact(DisplayName = "Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        var command = new DeleteOrderCommand(Guid.Empty);
        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}

using Ambev.DeveloperEvaluation.Application.Orders.ServiceOrder;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Orders.ServiceOrder;

public class DiscountServiceTests
{
    private readonly DiscountService _service = new DiscountService();

    [Theory(DisplayName = "Quantities below discount threshold should have no discount")]
    [InlineData(1, 10.0)]
    [InlineData(2, 5.5)]
    [InlineData(3, 100)]
    public void Given_QuantityLessThan4_When_Calculating_NoDiscount(decimal qty, double unitPrice)
    {
        var result = _service.Calculate(qty, (decimal)unitPrice);

        result.DiscountPercent.Should().Be(0m);
        result.DiscountAmount.Should().Be(0m);
        result.TotalAfterDiscount.Should().Be(result.TotalBeforeDiscount);
    }

    [Theory(DisplayName = "Quantities between 4 and 9 inclusive should get 10% discount")]
    [InlineData(4, 10.0, 4.00)]
    [InlineData(7, 5.0, 3.50)]
    [InlineData(9, 2.25, 2.03)]
    public void Given_QuantityBetween4And9_When_Calculating_TenPercentDiscount(decimal qty, double unitPrice, double expectedDiscount)
    {
        var result = _service.Calculate(qty, (decimal)unitPrice);

        result.DiscountPercent.Should().Be(0.10m);
        // compare rounded values
        result.DiscountAmount.Should().Be(Math.Round((decimal)expectedDiscount, 2));
        result.TotalAfterDiscount.Should().Be(result.TotalBeforeDiscount - result.DiscountAmount);
    }

    [Theory(DisplayName = "Quantities between 10 and 20 inclusive should get 20% discount")]
    [InlineData(10, 5.0, 10.00)]
    [InlineData(15, 2.0, 6.00)]
    [InlineData(20, 1.5, 6.00)]
    public void Given_QuantityBetween10And20_When_Calculating_TwentyPercentDiscount(decimal qty, double unitPrice, double expectedDiscount)
    {
        var result = _service.Calculate(qty, (decimal)unitPrice);

        result.DiscountPercent.Should().Be(0.20m);
        result.DiscountAmount.Should().Be(Math.Round((decimal)expectedDiscount, 2));
        result.TotalAfterDiscount.Should().Be(result.TotalBeforeDiscount - result.DiscountAmount);
    }

    [Fact(DisplayName = "Quantity above maximum should throw InvalidOperationException")]
    public void Given_QuantityAboveMax_When_Calculating_Throws()
    {
        Action act = () => _service.Calculate(21m, 10m);
        act.Should().Throw<InvalidOperationException>().WithMessage("*above 20*");
    }

    [Fact(DisplayName = "Quantity less than 1 should throw ArgumentOutOfRangeException")]
    public void Given_QuantityLessThanOne_When_Calculating_Throws()
    {
        Action act = () => _service.Calculate(0m, 10m);
        act.Should().Throw<ArgumentOutOfRangeException>().Where(e => e.ParamName == "quantity");
    }

    [Fact(DisplayName = "Negative unit price should throw ArgumentOutOfRangeException")]
    public void Given_NegativeUnitPrice_When_Calculating_Throws()
    {
        Action act = () => _service.Calculate(5m, -1m);
        act.Should().Throw<ArgumentOutOfRangeException>().Where(e => e.ParamName == "unitPrice");
    }

    [Fact(DisplayName = "Discount rounding behavior is away from zero to 2 decimals")]
    public void Given_PriceRequiresRounding_When_Calculating_RoundsAwayFromZero()
    {
        // unit price that leads to fractional discount that must be rounded
        var result = _service.Calculate(10m, 0.333m); // totalBefore = 3.33, discount 20% = 0.666 -> 0.67

        result.DiscountPercent.Should().Be(0.20m);
        result.DiscountAmount.Should().Be(0.67m);
        result.TotalAfterDiscount.Should().Be(2.66m);
    }
}

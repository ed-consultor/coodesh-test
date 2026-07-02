using System;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class OrderItemTests
{
    [Fact(DisplayName = "Generated order item should calculate total correctly")]
    public void Given_ValidOrderItem_When_Generated_TotalShouldBeCalculated()
    {
        var item = OrderItemTestData.GenerateValidOrderItem();

        var expected = Math.Round(item.Quantity * item.UnitPrice - item.Discount, 2);
        Assert.Equal(expected, Math.Round(item.TotalItemAmount, 2));
    }

    [Fact(DisplayName = "Order item with explicit values should calculate total correctly")]
    public void Given_OrderItemWithValues_When_Calculated_TotalShouldMatch()
    {
        var item = OrderItemTestData.GenerateOrderItem(quantity: 2.5m, unitPrice: 10.0m, discount: 1.0m, product: "TestProd");

        Assert.Equal("TestProd", item.Product);
        Assert.Equal(2.5m, item.Quantity);
        Assert.Equal(10.0m, item.UnitPrice);
        Assert.Equal(1.0m, item.Discount);

        var expected = Math.Round(item.Quantity * item.UnitPrice - item.Discount, 2);
        Assert.Equal(expected, item.TotalItemAmount);
    }

    [Fact(DisplayName = "Order item with zero quantity or price should compute zero or negative total accordingly")]
    public void Given_OrderItemWithZeroValues_When_Calculated_TotalShouldReflectValues()
    {
        var zeroQty = OrderItemTestData.GenerateOrderItem(0m, 10m);
        var zeroPrice = OrderItemTestData.GenerateOrderItem(2m, 0m);

        Assert.Equal(0m, zeroQty.TotalItemAmount);
        Assert.Equal(0m, zeroPrice.TotalItemAmount);
    }
}

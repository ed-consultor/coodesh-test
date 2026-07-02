using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class OrderItemTestData
{
    private static readonly Faker<OrderItem> ItemFaker = new Faker<OrderItem>()
        .RuleFor(i => i.Product, f => f.Commerce.ProductName())
        .RuleFor(i => i.Quantity, f => Math.Round(f.Random.Decimal(1, 10), 2))
        .RuleFor(i => i.UnitPrice, f => Math.Round(f.Random.Decimal(1, 200), 2))
        .RuleFor(i => i.Discount, f => 0m)
        .RuleFor(i => i.TotalItemAmount, (f, i) => Math.Round(i.Quantity * i.UnitPrice - i.Discount, 2));

    /// <summary>
    /// Generates a single valid OrderItem for use in tests.
    /// </summary>
    public static OrderItem GenerateValidOrderItem()
    {
        return ItemFaker.Generate();
    }

    /// <summary>
    /// Generates an OrderItem with the specified values.
    /// Useful to create edge-case items for negative tests.
    /// </summary>
    public static OrderItem GenerateOrderItem(decimal quantity, decimal unitPrice, decimal discount = 0m, string? product = null)
    {
        var item = new OrderItem
        {
            Product = product ?? ItemFaker.Generate().Product,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Discount = discount
        };
        item.TotalItemAmount = Math.Round(item.Quantity * item.UnitPrice - item.Discount, 2);
        return item;
    }
}

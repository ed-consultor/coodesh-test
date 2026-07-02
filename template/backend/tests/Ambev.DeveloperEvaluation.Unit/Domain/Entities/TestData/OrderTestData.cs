using System;
using System.Linq;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods to generate Order and OrderItem test data using Bogus.
    /// This supports creating valid and specific invalid scenarios for unit tests.
    /// </summary>
    public static class OrderTestData
    {
        private static readonly Faker<OrderItem> OrderItemFaker = new Faker<OrderItem>()
            .RuleFor(i => i.Product, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => Math.Round(f.Random.Decimal(1, 10), 2))
            .RuleFor(i => i.UnitPrice, f => Math.Round(f.Random.Decimal(1, 200), 2))
            .RuleFor(i => i.Discount, f => 0m)
            .RuleFor(i => i.TotalItemAmount, (f, i) => Math.Round(i.Quantity * i.UnitPrice - i.Discount, 2));

        private static readonly Faker<Order> OrderFaker = new Faker<Order>()
            .RuleFor(o => o.SaleNumber, f => f.Random.Replace("SN#####"))
            .RuleFor(o => o.SaleDate, f => f.Date.Past(1))
            .RuleFor(o => o.Customer, f => f.Person.FullName)
            .RuleFor(o => o.Branch, f => f.Company.CompanyName())
            .RuleFor(o => o.IsCancelled, f => false);

        /// <summary>
        /// Generates a valid Order with the specified number of items.
        /// TotalAmount is calculated from the generated items.
        /// </summary>
        public static Order GenerateValidOrder(int itemsCount = 2)
        {
            var items = OrderItemFaker.Generate(itemsCount);
            var order = OrderFaker.Generate();
            order.Id = Guid.NewGuid();
            order.Branch = "Main Branch";
            order.Items = items;
            order.TotalAmount = items.Sum(i => i.TotalItemAmount);
            return order;
        }

        /// <summary>
        /// Generates an Order marked as cancelled.
        /// </summary>
        public static Order GenerateCancelledOrder(int itemsCount = 1)
        {
            var order = GenerateValidOrder(itemsCount);
            order.IsCancelled = true;
            return order;
        }

        /// <summary>
        /// Generates an Order with no items (useful to test validation rules enforcing items presence).
        /// </summary>
        public static Order GenerateOrderWithNoItems()
        {
            var order = OrderFaker.Generate();
            order.Items = new List<OrderItem>();
            order.TotalAmount = 0m;
            return order;
        }

        /// <summary>
        /// Generates an Order with an invalid negative total amount (useful for negative tests).
        /// </summary>
        public static Order GenerateOrderWithNegativeTotal()
        {
            var order = GenerateValidOrder();
            order.TotalAmount = -Math.Abs(order.TotalAmount);
            return order;
        }
    }

}

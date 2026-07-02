using System;
using System.Linq;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class OrderTests
    {
        [Fact(DisplayName = "Generated valid order should have total equal to sum of item totals")]
        public void Given_ValidOrder_When_Generated_TotalShouldMatchItemsSum()
        {
            // Arrange
            var order = OrderTestData.GenerateValidOrder(itemsCount: 3);

            // Act
            var itemsSum = order.Items.Sum(i => i.TotalItemAmount);

            // Assert
            Assert.Equal(3, order.Items.Count);
            Assert.Equal(itemsSum, order.TotalAmount);
        }

        [Fact(DisplayName = "Cancelled order should be marked as cancelled")]
        public void Given_CancelledOrder_When_Generated_ShouldBeCancelled()
        {
            // Arrange
            var order = OrderTestData.GenerateCancelledOrder(itemsCount: 1);

            // Assert
            Assert.True(order.IsCancelled);
            Assert.NotEmpty(order.Items);
        }

        [Fact(DisplayName = "Order generated with no items should have zero total")]
        public void Given_OrderWithNoItems_When_Generated_TotalShouldBeZero()
        {
            // Arrange
            var order = OrderTestData.GenerateOrderWithNoItems();

            // Assert
            Assert.Empty(order.Items);
            Assert.Equal(0m, order.TotalAmount);
        }

        [Fact(DisplayName = "Order with negative total should have negative TotalAmount")]
        public void Given_OrderWithNegativeTotal_When_Generated_TotalShouldBeNegative()
        {
            // Arrange
            var order = OrderTestData.GenerateOrderWithNegativeTotal();

            // Assert
            Assert.True(order.TotalAmount < 0);
        }

        [Fact(DisplayName = "Generated valid order should have required fields populated")]
        public void Given_ValidOrder_When_Generated_RequiredFieldsArePopulated()
        {
            // Arrange
            var order = OrderTestData.GenerateValidOrder(itemsCount: 2);

            // Assert basic required fields
            Assert.False(string.IsNullOrWhiteSpace(order.SaleNumber));
            Assert.NotEqual(default(DateTime), order.SaleDate);
            Assert.False(string.IsNullOrWhiteSpace(order.Customer));
            Assert.False(string.IsNullOrWhiteSpace(order.Branch));
            Assert.True(order.TotalAmount >= 0);
        }

        [Fact(DisplayName = "Order items should have positive quantity and unit price and correct total calculation")]
        public void Given_ValidOrder_When_Generated_ItemsShouldBeValid()
        {
            // Arrange
            var order = OrderTestData.GenerateValidOrder(itemsCount: 4);

            // Act & Assert per item
            foreach (var item in order.Items)
            {
                Assert.False(string.IsNullOrWhiteSpace(item.Product));
                Assert.True(item.Quantity > 0);
                Assert.True(item.UnitPrice > 0);
                var expectedTotal = Math.Round(item.Quantity * item.UnitPrice - item.Discount, 2);
                Assert.Equal(expectedTotal, Math.Round(item.TotalItemAmount, 2));
            }
        }
    }
}

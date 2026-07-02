using Ambev.DeveloperEvaluation.Application.Orders.ServiceOrder;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Implements quantity-based discounting business rules:
    /// - No discount for quantity &lt; 4
    /// - 10% discount for quantity >= 4
    /// - 20% discount for quantity between 10 and 20 (inclusive)
    /// - Maximum allowed quantity per product is 20
    /// </summary>
    public sealed class DiscountService : IDiscountService
    {
        private const decimal MinQuantity = 1m;
        private const decimal NoDiscountThreshold = 4m;
        private const decimal SecondTierThreshold = 10m;
        private const decimal MaxQuantity = 20m;

        public DiscountResult Calculate(decimal quantity, decimal unitPrice)
        {
            if (quantity < MinQuantity)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be at least 1.");

            if (quantity > MaxQuantity)
                throw new InvalidOperationException($"Cannot sell above {MaxQuantity} identical items.");

            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            decimal discountPercent = GetDiscountPercent(quantity);

            decimal totalBefore = unitPrice * quantity;
            decimal discountAmount = Math.Round(totalBefore * discountPercent, 2, MidpointRounding.AwayFromZero);
            decimal totalAfter = totalBefore - discountAmount;

            return new DiscountResult
            {
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalBeforeDiscount = totalBefore,
                DiscountPercent = discountPercent,
                DiscountAmount = discountAmount,
                TotalAfterDiscount = totalAfter
            };
        }

        private static decimal GetDiscountPercent(decimal quantity)
        {
            if (quantity < NoDiscountThreshold)
                return 0m;

            if (quantity >= SecondTierThreshold && quantity <= MaxQuantity)
                return 0.20m;

            // quantity is between NoDiscountThreshold (inclusive) and SecondTierThreshold-1
            return 0.10m;
        }
    }
}

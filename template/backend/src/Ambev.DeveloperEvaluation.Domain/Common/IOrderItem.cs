namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public interface IOrderItem
    {
        /// <summary>
        /// Product name/description
        /// </summary>
        string Product { get; set; }

        /// <summary>
        /// Quantity of the product
        /// </summary>
        decimal Quantity { get; set; }

        /// <summary>
        /// Unit price of the product
        /// </summary>
        decimal UnitPrice { get; set; }

        /// <summary>
        /// Discount applied to this item
        /// </summary>
        decimal Discount { get; set; }

        /// <summary>
        /// Total amount for this item (Quantity * UnitPrice - Discount)
        /// </summary>
        decimal TotalItemAmount { get; set; }
    }
}

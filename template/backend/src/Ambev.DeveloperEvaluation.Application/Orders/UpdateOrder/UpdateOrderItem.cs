namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
    public class UpdateOrderItem
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Product { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Gets or sets the discount for the item.
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// Gets or sets the total amount for the item.
        /// </summary>  
        public decimal TotalItemAmount { get; set; }
    }
}

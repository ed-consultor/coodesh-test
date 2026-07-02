namespace Ambev.DeveloperEvaluation.Application.Orders.ServiceOrder
{
    public class DiscountResult
    {
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalBeforeDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAfterDiscount { get; set; }
    }
}

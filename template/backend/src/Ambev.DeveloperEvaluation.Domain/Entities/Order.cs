using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Order : BaseEntity, IOrder
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; } 
        public string Customer { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; } 
        public string Branch { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new() { };
        public bool IsCancelled { get; set; }
    }
}

using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Response model for GetOrder operation
/// </summary>
public class GetOrderResult
{
    /// <summary>
    /// The unique identifier of the order
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The sale number associated with the order
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the sale was made
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The customer name or identifier for the order
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The total amount for the order
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The branch where the order was placed
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// The collection of items included in the order
    /// </summary>
    public List<OrderItemResult> Items { get; set; } = new();

    /// <summary>
    /// Indicates whether the order has been cancelled
    /// </summary>
    public bool IsCancelled { get; set; }
}

public class OrderItemResult
{
    /// <summary>
    /// The product identifier or name for the item
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// The quantity ordered for this item
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// The unit price for the item
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to this item
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total amount for this line item (after discount)
    /// </summary>
    public decimal TotalItemAmount { get; set; }
}

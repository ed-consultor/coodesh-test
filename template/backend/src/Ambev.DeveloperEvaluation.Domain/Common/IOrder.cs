namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public interface IOrder
    {
        /// <summary>
        /// Sale number/identifier
        /// </summary>
        string SaleNumber { get; set; }

        /// <summary>
        /// Date when the sale was made
        /// </summary>
        DateTime SaleDate { get; set; }

        /// <summary>
        /// Customer information
        /// </summary>
        string Customer { get; set; }

        /// <summary>
        /// Total sale amount
        /// </summary>
        decimal TotalAmount { get; set; }

        /// <summary>
        /// Branch where the sale was made
        /// </summary>
        string Branch { get; set; }

        /// <summary>
        /// Indicates if the sale is cancelled or not
        /// </summary>
        bool IsCancelled { get; set; }
    }
}

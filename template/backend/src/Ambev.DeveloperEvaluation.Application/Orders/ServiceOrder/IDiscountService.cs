using Ambev.DeveloperEvaluation.Application.Orders.ServiceOrder;
namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Calculates quantity-based discounts according to business rules.
    /// </summary>
    public interface IDiscountService
    {
        DiscountResult Calculate(decimal quantity, decimal unitPrice);
    }
}

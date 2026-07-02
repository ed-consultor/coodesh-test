using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Creates a new order in the repository
        /// </summary>
        /// <param name="order">The order to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created order</returns>
        Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing order in the repository
        /// </summary>
        /// <param name="order">The order to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated order</returns>
        Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an order by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the order</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The order if found, null otherwise</returns>      

        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an order by its code
        /// </summary>
        /// <param name="code">The code to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The order if found, null otherwise</returns>
        Task<Order?> GetBySaleNumberAsync(string code, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an order from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the order to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the order was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}

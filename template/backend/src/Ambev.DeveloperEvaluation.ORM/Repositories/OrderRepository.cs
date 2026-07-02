using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of OrderRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public OrderRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new order in the database
        /// </summary>
        /// <param name="order">The order to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created order</returns>
        public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }

        /// <summary>
        /// Retrieves an order by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the order</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The order if found, null otherwise</returns>
        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(x => x.Items).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves an order by its code
        /// </summary>
        /// <param name="code">The code to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The order if found, null otherwise</returns>
        public async Task<Order?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.SaleNumber == saleNumber, cancellationToken);
        }

        /// <summary>
        /// Deletes an order from the database
        /// </summary>
        /// <param name="id">The unique identifier of the order to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the order was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await GetByIdAsync(id, cancellationToken);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Update an order from the database
        /// </summary>
        /// <param name="id">The unique identifier of the order to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the order was updated, false if not found</returns>

        public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {           
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}

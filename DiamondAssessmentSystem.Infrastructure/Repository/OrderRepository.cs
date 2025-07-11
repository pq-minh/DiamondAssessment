using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public OrderRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.Customer)       
                .Include(o => o.Payments)
                .Include(o => o.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomers(string userId)
        {
            var customerId = await GetCustomerId(userId);

            return await _context.Orders
                .Include(o => o.Customer.CustomerId == customerId)
                .Include(o => o.Payments)
                .Include(o => o.Service)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payments)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<int> GetCurentOrderId(string? userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
            
            if (customer == null)
            {
                return -1;
            }

            var order = await _context.Orders.Where(od => od.CustomerId == customer.CustomerId).OrderByDescending(od => od.OrderDate).FirstOrDefaultAsync();
            int orderId = 0;

            if (order != null)
            {
                orderId = order.OrderId;
                if (orderId <= 0)
                {
                    orderId = 0;
                }
            }

            var orderIdNext = orderId += 1;
            return orderIdNext;
        }

        public async Task<bool> CreateOrderAsync(string userId, Order order)
        {
            var customerId = await GetCustomerId(userId);
            order.CustomerId = customerId;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExistsAsync(order.OrderId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            order.Status = "Canceled";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> OrderExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(e => e.OrderId == id);
        }

        private async Task<int> GetCustomerId(string userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (customer == null)
            {
                return -1;
            }

            return customer.CustomerId;
        }
    }
}

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

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)       
                .Include(o => o.Payments)
                .Include(o => o.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomers(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Customer)
                .Include(o => o.Payments)
                .Include(o => o.Service)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Payments)
                .Include(o => o.Service)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<int> GetCurentOrderId(string userId)
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
                if (orderId <= 0 || orderId == null)
                {
                    orderId = 0;
                }
            }

            var orderIdNext = orderId += 1;
            return orderIdNext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
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

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> OrderExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(e => e.OrderId == id);
        }
    }
}

using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByCustomers(string userId);
        Task<int> GetCurentOrderId(string? userId);
        Task<bool> CreateOrderAsync(string userId, Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrder(int id);
    }
}

using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByCustomers(int customerId);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}

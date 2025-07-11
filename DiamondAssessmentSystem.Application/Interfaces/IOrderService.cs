using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersByCustomers(string userId);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<int> GetCurentOrderId();
        Task<bool> CreateOrder(string userId, int requestId, OrderCreateDto orderCreateDto, string paymentType, VnPaymentResponseFromFe request);
        Task<bool> UpdateOrderAsync(int id, OrderCreateDto orderCreateDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<bool> UpdatePayment(string userId, int orderId, String status);
    }
}

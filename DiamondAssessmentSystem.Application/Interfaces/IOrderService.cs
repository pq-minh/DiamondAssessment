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
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<int> GetCurentOrderId();
        Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<bool> UpdateOrderAsync(int id, OrderCreateDto orderCreateDto);
        Task<bool> DeleteOrderAsync(int id);
    }
}

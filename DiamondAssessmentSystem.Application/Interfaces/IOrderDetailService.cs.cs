using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync();
        Task<OrderDetailDto> GetOrderDetailByIdAsync(int id);
        Task<OrderDetailDto> CreateOrderDetailAsync(OrderDetailCreateDto orderDetailCreateDto);
        Task<bool> UpdateOrderDetailAsync(int id, OrderDetailCreateDto orderDetailCreateDto);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}

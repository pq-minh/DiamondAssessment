using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        // GET: api/OrderDetail
        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails); // Sử dụng AutoMapper để chuyển đổi từ entity sang DTO
        }

        // GET: api/OrderDetail/5
        public async Task<OrderDetailDto> GetOrderDetailByIdAsync(int id)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return null;  // Nếu không tìm thấy order detail, trả về null
            }

            return _mapper.Map<OrderDetailDto>(orderDetail); // Sử dụng AutoMapper để chuyển đổi từ entity sang DTO
        }

        // POST: api/OrderDetail
        public async Task<OrderDetailDto> CreateOrderDetailAsync(OrderDetailCreateDto orderDetailCreateDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailCreateDto);  // Map từ DTO sang entity

            var createdOrderDetail = await _orderDetailRepository.CreateOrderDetailAsync(orderDetail);

            return _mapper.Map<OrderDetailDto>(createdOrderDetail); // Map từ entity sang DTO
        }

        // PUT: api/OrderDetail/5
        public async Task<bool> UpdateOrderDetailAsync(int id, OrderDetailCreateDto orderDetailCreateDto)
        {
            var existingOrderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);
            if (existingOrderDetail == null)
            {
                return false;  // Nếu không tìm thấy order detail, trả về false
            }

            // Cập nhật các thông tin trong entity từ DTO
            _mapper.Map(orderDetailCreateDto, existingOrderDetail);  // Map DTO vào entity hiện tại

            return await _orderDetailRepository.UpdateOrderDetailAsync(existingOrderDetail);  // Cập nhật vào DB
        }

        // DELETE: api/OrderDetail/5
        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await _orderDetailRepository.DeleteOrderDetailAsync(id);  // Xóa order detail
        }
    }
}

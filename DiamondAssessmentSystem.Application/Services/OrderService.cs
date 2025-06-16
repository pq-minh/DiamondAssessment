using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // GET: api/Order
        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders); // Ánh xạ từ Order sang OrderDto
        }

        // GET: api/Order/5
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                return null; // Trả về null nếu không tìm thấy Order
            }

            return _mapper.Map<OrderDto>(order); // Ánh xạ từ Order sang OrderDto
        }

        // POST: api/Order
        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            // Kiểm tra các điều kiện đầu vào
            if (orderCreateDto == null)
            {
                throw new ArgumentNullException(nameof(orderCreateDto), "OrderCreateDto is null.");
            }

            if (string.IsNullOrWhiteSpace(orderCreateDto.OrderDetailId))
            {
                throw new ArgumentException("OrderDetailId is required.");
            }

            var orderDetailIds = orderCreateDto.OrderDetailId.Split(',')
                .Select(id => id.Trim())
                .Where(id => !string.IsNullOrEmpty(id))
                .Select(int.Parse)
                .ToList();

            // Kiểm tra tính hợp lệ của các OrderDetailId
            //foreach (var orderDetailId in orderDetailIds)
            //{
            //    var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
            //    if (orderDetail == null)
            //    {
            //        throw new ArgumentException($"OrderDetailId {orderDetailId} is invalid.");
            //    }
            //}

            var order = _mapper.Map<Order>(orderCreateDto); // Ánh xạ từ DTO sang Entity

            var createdOrder = await _orderRepository.CreateOrderAsync(order);

            if (createdOrder == null)
            {
                throw new InvalidOperationException("A problem happened while handling your request.");
            }

            return _mapper.Map<OrderDto>(createdOrder); // Ánh xạ từ Order sang OrderDto
        }

        // PUT: api/Order/5
        public async Task<bool> UpdateOrderAsync(int id, OrderCreateDto orderCreateDto)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);

            if (existingOrder == null)
            {
                return false; // Nếu không tìm thấy Order
            }

            // Kiểm tra và cập nhật OrderDetailId
            var orderDetailIds = orderCreateDto.OrderDetailId.Split(',').Select(int.Parse).ToList();

            //foreach (var orderDetailId in orderDetailIds)
            //{
            //    var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
            //    if (orderDetail == null)
            //    {
            //        throw new ArgumentException($"OrderDetailId {orderDetailId} is invalid.");
            //    }
            //}

            _mapper.Map(orderCreateDto, existingOrder); // Ánh xạ từ DTO vào Order Entity hiện tại

            return await _orderRepository.UpdateOrderAsync(existingOrder); // Cập nhật Order
        }

        // DELETE: api/Order/5
        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id); // Xóa Order
        }
    }
}

using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using DiamondAssessmentSystem.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IServicePriceRepository _servicePriceRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IRequestRepository requestRepository, IServicePriceRepository servicePriceRepository, IMapper mapper, ICurrentUserService currentUser)
        {
            _orderRepository = orderRepository;
            _requestRepository = requestRepository;
            _servicePriceRepository = servicePriceRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }


        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();

            var servicePrices = await _servicePriceRepository.GetServicePricesAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders).ToList();

            foreach (var dto in orderDtos)
            {
                var service = servicePrices.FirstOrDefault(x => x.ServiceId == dto.ServiceId);
                dto.ServiceType = service?.ServiceType;
            }

            return orderDtos;
            //return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        // GET: api/Order/5
        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                return null;
            }

            var service = await _servicePriceRepository.GetServicePriceByIdAsync(order.ServiceId);

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.ServiceType = service?.ServiceType;

            return orderDto;

            //return _mapper.Map<OrderDto>(order);
        }

        public async Task<int> GetCurentOrderId()
        {
            var orderId = await _orderRepository.GetCurentOrderId(_currentUser.UserId);
            return orderId;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var request = await _requestRepository.GetRequestByIdAsync(orderCreateDto.RequestId);

            if (request == null)
                throw new InvalidOperationException("Request not found.");

            if (request.Customer == null)
                throw new InvalidOperationException("Customer information missing in request.");

            if (request == null || request.Status != "Pending")
                throw new InvalidOperationException("Invalid request.");

            if (request.Status != "Pending")
                throw new InvalidOperationException($"Cannot create order. Request status is '{request.Status}', expected 'Pending'.");

            var service = await _servicePriceRepository.GetServicePriceByIdAsync(request.ServiceId);

            if (service == null)
                throw new InvalidOperationException("Service not found for the request.");

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                ServiceId = request.ServiceId,
                Status = "Pending",
                TotalPrice = service.Price
            };

            var createdOrder = await _orderRepository.CreateOrderAsync(order);
            return _mapper.Map<OrderDto>(createdOrder);
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderCreateDto orderCreateDto)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);

            if (existingOrder == null)
            {
                return false;
            }

            _mapper.Map(orderCreateDto, existingOrder);

            return await _orderRepository.UpdateOrderAsync(existingOrder);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return false;
            order.Status = status;
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }
    }
}

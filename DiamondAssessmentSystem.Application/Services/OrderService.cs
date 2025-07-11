using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using PhoneNumbers;

namespace DiamondAssessmentSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ICurrentUserService currentUser, IRequestRepository requestRepository, IVnPayService vnPayService, IPaymentRepository paymentRepository)
        {
            _orderRepository = orderRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _requestRepository = requestRepository;
            _vnPayService = vnPayService;
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrders();
            return _mapper.Map<IEnumerable<OrderDto>>(orders); 
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderById(id);

            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderDto>(order); 
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomers(string userId)
        {
            var orders = await _orderRepository.GetOrdersByCustomers(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<int> GetCurentOrderId()
        {
            var orderId = await _orderRepository.GetCurentOrderId(_currentUser.UserId);
            return orderId;
        }

        public async Task<bool> CreateOrder(string userId, int requestId, OrderCreateDto orderCreateDto, string paymentType, VnPaymentResponseFromFe request)
        {
            if (!(await UpdateRequest(requestId))) return false;

            if (orderCreateDto == null)
            {
                throw new ArgumentNullException(nameof(orderCreateDto), "OrderCreateDto is null.");
            }

            var order = _mapper.Map<Order>(orderCreateDto);

            if (paymentType == "Online")
            {
                var response = _vnPayService.ExecutePayment(request);

                if (response == null || !response.Success)
                {
                    return false;
                }

                var createdOrder = await _orderRepository.CreateOrderAsync(userId, order);

                if (createdOrder != true)
                {
                    throw new InvalidOperationException("A problem happened while handling your request.");
                }

                var status = "Completed";
                var paymentUpdate = await _paymentRepository.UpdatePayment(userId, status, paymentType);

                return paymentUpdate;
            }
            else if(paymentType == "Ofline")
            {
                order.Status = "Pending";

                var createdOrder = await _orderRepository.CreateOrderAsync(userId, order);

                if (createdOrder != true)
                {
                    throw new InvalidOperationException("A problem happened while handling your request.");
                }

                var status = "Pending";
                var paymentUpdate = await _paymentRepository.UpdatePayment(userId, status, paymentType);

                return paymentUpdate;
            }
            else
            {
                throw new ArgumentNullException(nameof(orderCreateDto), "Wrong type");
            }
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderCreateDto orderCreateDto)
        {
            var existingOrder = await _orderRepository.GetOrderById(id);

            if (existingOrder == null)
            {
                return false; 
            }

            _mapper.Map(orderCreateDto, existingOrder); 

            return await _orderRepository.UpdateOrderAsync(existingOrder); 
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrder(id); 
        }

        public async Task<bool> UpdatePayment(string userId, int orderId, String status)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return false;
            }

            var method = "Ofline";

            return await _paymentRepository.UpdatePayment(userId, status, method);
        }

        private async Task<bool> UpdateRequest(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);

            if (request == null)
            {
                return false;
            }

            request.Status = "Pending";
            await _requestRepository.UpdateRequestAsync(request);

            return true;
        }

        public async Task<PaymentDto> PayByVnpay(string userId, VnPaymentResponseFromFe request)
        {
            if (request == null)
            {
                return new PaymentDto
                {
                    Status = "Failed",
                    Message = "Paramaters can not identify"
                };
            }
            var response = _vnPayService.ExecutePayment(request);
            if (response == null || !response.Success)
            {
                return new PaymentDto
                {
                    Status = "Failed",
                    Message = $"PaymentFail {response?.VnPayResponseCode}"
                };

            }
            var paymentUpdate = await _paymentRepository.UpdatePayment(userId, "Completed", "Online");
            if (paymentUpdate)
            {
                return new PaymentDto
                {
                    Status = "Completed",
                    Message = "Payment successful."
                };
            }
            else
            {
                return new PaymentDto
                {
                    Status = "Failed",
                    Message = "Payment unsuccessful."
                };
            }
        }

        private bool IsPhoneNumberValid(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedNumber = phoneNumberUtil.Parse(phoneNumber, regionCode);
                return phoneNumberUtil.IsValidNumber(parsedNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

    }
}

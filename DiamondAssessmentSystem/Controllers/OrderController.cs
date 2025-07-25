using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUser;
        private readonly IVnPayService _vnPayService;

        public OrderController(IOrderService orderService, ICurrentUserService currentUser, IVnPayService vnPayService)
        {
            _orderService = orderService;
            _currentUser = currentUser;
            _vnPayService = vnPayService;

        }

        // GET: api/order/me
        [HttpGet("me")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
        {
            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var orders = await _orderService.GetOrdersByCustomerAsync(userId);
            return Ok(orders);
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders()
        {
            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var orders = await _orderService.GetOrdersByCustomerAsync(userId);
            return Ok(orders);
        }

        // GET: api/order/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateCombineDto order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var created = await _orderService.CreateOrderAsync(
                userId,
                order.PaymentInfo.RequestId,
                order.OrderData,
                order.PaymentInfo.PaymentType,
                order.PaymentInfo.PaymentRequest);

            if (!created)
                return BadRequest("Could not create order.");

            return NoContent();
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderCreateDto orderDto)
        {
            var userId = _currentUser.UserId;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _orderService.UpdateOrderAsync(id, orderDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var canceled = await _orderService.CancelOrderAsync(id);
            if (!canceled)
                return NotFound();

            return NoContent();
        }

        // PUT: api/order/payment
        [HttpPut("payment")]
        public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var updated = await _orderService.UpdatePaymentAsync(userId, paymentDto.OrderId, paymentDto.Status);
            if (!updated)
                return BadRequest();

            return NoContent();
        }

        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] VnPaymentRequestDto dto)
        {
            var url = await _vnPayService.CreatePatmentUrl(HttpContext, dto);
            return Ok(new { paymentUrl = url });
        }
    }
}

using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.Models;
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

        public OrderController(IOrderService orderService, ICurrentUserService currentUser)
        {
            _orderService = orderService;
            _currentUser = currentUser;
        }

        //[Authorize(Roles = "Manager")]
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderByCustomers()
        {
            var userIdClaim = _currentUser.UserId;

            if (userIdClaim == null)
                return Unauthorized();

            var orders = await _orderService.GetOrdersByCustomers(userIdClaim);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] orderCreateCombine order)
        {
            var userId = _currentUser.UserId;

            if (userId == null)
                return Unauthorized();

            var requestId = order.orderPaymentDto.requestId;
            var paymentType = order.orderPaymentDto.paymentType;
            var request = order.orderPaymentDto.request;
            var orderCreateDto = order.OrderCreateDto;

            var createdOrder = await _orderService.CreateOrder(userId, requestId, orderCreateDto, paymentType, request);

            if (createdOrder)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderCreateDto orderCreateDto)
        {
            var updated = await _orderService.UpdateOrderAsync(id, orderCreateDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("Payment")]
        public async Task<IActionResult> UpdatePayment(int orderId, string status)
        {
            var userIdClaim = _currentUser.UserId;

            if (userIdClaim == null)
                return Unauthorized();

            var updated = await _orderService.UpdatePayment(userIdClaim, orderId, status);

            if (!updated)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}

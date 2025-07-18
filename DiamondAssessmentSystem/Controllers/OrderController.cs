using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/Order
        [HttpGet]
        //[Authorize(Roles = "Admin,Consultant")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Order/{id}
        [HttpGet("GetById/{id}")]
        //[Authorize(Roles = "Admin,Consultant")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/Order
        [HttpPost("Create-order")]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderCreateDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        //// PUT: api/Order/5
        //[HttpPut("Update-order/{id}")]
        //public async Task<IActionResult> UpdateOrder(int id, OrderCreateDto orderCreateDto)
        //{
        //    var updated = await _orderService.UpdateOrderAsync(id, orderCreateDto);
        //    if (!updated)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        [HttpPatch("{id}/status")]
        //[Authorize(Roles = "Admin,Consultant")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, status);
            return result ? Ok() : NotFound();
        }

        // DELETE: api/Order/5
        [HttpDelete("Delete-order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

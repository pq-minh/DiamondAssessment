using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICurrentUserService _currentUser;

        public CustomerController(ICustomerService customerService, ICurrentUserService currentUser)
        {
            _customerService = customerService;
            _currentUser = currentUser;
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer()
        {
            var userIdClaim = _currentUser.UserId;

            if (userIdClaim == null)
                return Unauthorized();

            var customer = await _customerService.GetCustomerByIdAsync(userIdClaim);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerCreateDto customerCreateDto)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customerCreateDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(CustomerCreateDto customerCreateDto)
        {
            var userId = _currentUser.UserId;

            if (userId == null)
                return Unauthorized();

            var updated = await _customerService.UpdateCustomerAsync(userId, customerCreateDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
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

        //// GET: api/Customer/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CustomerDto>> GetCustomer()
        //{
        //    var userIdClaim = _currentUser.UserId;

        //    if (userIdClaim == null)
        //        return Unauthorized();

        //    var customer = await _customerService.GetCustomerByIdAsync(userIdClaim);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(customer);
        //}

        //[HttpGet("Current/profile")]
        ////[Authorize(Roles = "Customer")]
        //public async Task<ActionResult<CustomerDto>> GetCurrentCustomer()
        //{
        //    var customerId = _currentUser.CustomerId;

        //    if (customerId == null)
        //        return Unauthorized("CustomerId not found in token.");

        //    if (!customerId.HasValue)
        //        return Unauthorized("You are not authorized or AssociatedId missing.");

        //    var customer = await _customerService.GetCustomerByCustomerIdAsync(customerId.Value);

        //    if (customer == null)
        //        return NotFound();

        //    return Ok(customer);
        //}

        ////[Authorize(Roles = "Customer")]
        //[HttpGet("my-profile")]
        //public async Task<IActionResult> GetMyCustomerProfile()
        //{
        //    if (_currentUser.CustomerId == null)
        //        return Unauthorized("CustomerId not found in token");

        //    var customer = await _customerService.GetCustomerByIdAsync(_currentUser.UserId);

        //    if (customer == null)
        //        return NotFound();

        //    return Ok(customer);
        //}



        [HttpGet("GetByUserId/{id}")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
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
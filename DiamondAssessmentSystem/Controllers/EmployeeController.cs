using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Enums;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Assessment")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICurrentUserService _currentUser;

        public EmployeeController(IEmployeeService employeeService, ICurrentUserService currentUser)
        {
            _employeeService = employeeService;
            _currentUser = currentUser;
        }

        // GET api/employee
        [HttpGet("me")]
        public async Task<IActionResult> GetMyEmployee()
        {
            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var employee = await _employeeService.GetEmployees(userId);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // PUT api/employee/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyEmployee([FromBody] EmployeeDto employeeDto)
        {
            var userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _employeeService.UpdateEmployee(userId, employeeDto);

            return result switch
            {
                EmployeeEnum.Success => NoContent(),
                EmployeeEnum.NotFound => NotFound("Customer not found."),
                EmployeeEnum.InvalidPhoneNumber => BadRequest("Invalid phone number."),
                EmployeeEnum.UpdateFailed => StatusCode(500, "Failed to update customer."),
                _ => StatusCode(500, "Unexpected error.")
            };
        }

        // PUT api/employee/{userId}
        [HttpPut("{userId}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(string userId, [FromBody] EmployeeDto employeeDto)
        {
            var result = await _employeeService.UpdateEmployee(userId, employeeDto);

            return result switch
            {
                EmployeeEnum.Success => NoContent(),
                EmployeeEnum.NotFound => NotFound("Customer not found."),
                EmployeeEnum.InvalidPhoneNumber => BadRequest("Invalid phone number."),
                EmployeeEnum.UpdateFailed => StatusCode(500, "Failed to update customer."),
                _ => StatusCode(500, "Unexpected error.")
            };
        }
    }
}
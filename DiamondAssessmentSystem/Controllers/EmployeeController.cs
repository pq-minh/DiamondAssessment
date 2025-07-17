using Microsoft.AspNetCore.Mvc;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Application.DTO;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Assement")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICurrentUserService _currentUser;

        public EmployeeController(IEmployeeService employeeService, ICurrentUserService currentUser)
        {
            _employeeService = employeeService;
            _currentUser = currentUser;
        }

        // GET: api/Employee/userId/{userId}
        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetEmployeeByUserId(string userId)
        {
            var employee = await _employeeService.GetEmployeeByUserIdAsync(userId);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // GET: api/Employee/{employeeId}
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        //PUT api/Employee/{employeeId}/assign-role (Manager)
        [HttpPut("assign-role/{employeeId}")]
        public async Task<IActionResult> AssignStaffRole(int employeeId, [FromBody] string newRole)
        {
            var result = await _employeeService.AssignStaffRoleAsync(employeeId, newRole);
            if (!result) return BadRequest("Employee not found or invalid role.");

            return Ok("Role updated successfully.");
        }

        //// PUT: api/Employee/5 (Manager)
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] UpdateEmployeeDto updateDto)
        {
            var currentUserId = _currentUser.UserId;

            var result = await _employeeService.UpdateEmployeeAsync(employeeId, updateDto, currentUserId);
            if (!result) return NotFound("Employee not found.");

            return Ok("Employee updated successfully.");
        }
    }
}
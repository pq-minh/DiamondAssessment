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

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee()
        {
            var userId = _currentUser.UserId;

            if (userId == null)
                return Unauthorized();

            var employee = await _employeeService.GetEmployee(userId);

            if (employee == null)
            {
                return NotFound(); 
            }

            return Ok(employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id == null)
            {
                return BadRequest("Employee ID mismatch.");
            }

            var updated = await _employeeService.PutEmployee(id, employeeDto);

            if (!updated)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }
    }
}

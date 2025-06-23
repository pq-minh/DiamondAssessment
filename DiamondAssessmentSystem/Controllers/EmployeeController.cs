using Microsoft.AspNetCore.Mvc;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Application.DTO;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployee(id);

            if (employee == null)
            {
                return NotFound(); // Trả về NotFound nếu không tìm thấy nhân viên
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null.");
            }

            var createdEmployee = await _employeeService.PostEmployee(employeeDto);

            if (createdEmployee == false)
            {
                return BadRequest("Error");
            }
            return Ok();
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
                return NotFound(); // Trả về NotFound nếu không tìm thấy nhân viên
            }

            return NoContent(); // Trả về NoContent khi cập nhật thành công
        }
    }
}

using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        //Task<EmployeeDto?> GetEmployee(string id);
        Task<EmployeeDto> GetEmployeeByUserIdAsync(string userId);
        Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId);
        //Task<bool> PutEmployee(int id, EmployeeDto employeeDto);
        Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateDto);
        Task<bool> AssignStaffRoleAsync(int employeeId, string newRole);
    }
}

using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployees(string id);
        Task<AccountDto?> GetUserById(int id);
        Task<EmployeeEnum> UpdateEmployee(string userId, EmployeeDto employeeDto);
        //Task<bool> DeleteEmployeeAsync(string userId);
        Task<string?> GetEmployeeEmail(string userId);
    }
}

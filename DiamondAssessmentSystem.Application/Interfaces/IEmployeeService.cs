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
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto?> GetEmployee(int id);
        Task<EmployeeDto> PostEmployee(EmployeeDto employeeDto);
        Task<bool> PutEmployee(int id, EmployeeDto employeeDto);
        Task<bool> DeleteEmployee(int id);

    }
}

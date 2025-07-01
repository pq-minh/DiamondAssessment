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
        Task<EmployeeDto?> GetEmployee(string id);
        Task<bool> PutEmployee(int id, EmployeeDto employeeDto);

    }
}

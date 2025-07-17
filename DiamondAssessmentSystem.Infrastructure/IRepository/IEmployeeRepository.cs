using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByUserIdAsync(string userId);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeUserRoleAsync(int employeeId, string newRole);

    }
}

using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using DiamondAssessmentSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository accountRepository, IMapper mapper, UserManager<User> userManager)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<EmployeeDto> GetEmployeeByUserIdAsync(string userId)
        {
            var employee = await _employeeRepository.GetEmployeeByUserIdAsync(userId);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<bool> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id == null)
            {
                return false;
            }

            var employee = _mapper.Map<Employee>(employeeDto); 

            var updated = await _employeeRepository.UpdateEmployeeAsync(employee);

            return updated; 
        }

        public async Task<bool> AssignStaffRoleAsync(int employeeId, string newRole)
        {
            if (newRole != "Assessor" && newRole != "Consultant")
                throw new ArgumentException("Role must be Assessor or Consultant.");

            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null) return false;

            var user = employee.User;
            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, newRole);

            // Đồng bộ UserType nếu muốn dùng song song
            user.UserType = newRole;
            await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null) return false;

            var user = employee.User;
            if (user == null) return false;

            _mapper.Map(updateDto, user);
            employee.Salary = updateDto.Salary;

            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }
}

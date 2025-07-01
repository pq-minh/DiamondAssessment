using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _accountRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository accountRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto?> GetEmployee(string userId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(userId);
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

    }
}

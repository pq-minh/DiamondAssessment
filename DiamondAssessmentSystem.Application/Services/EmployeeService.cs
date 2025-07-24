using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Enums;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using PhoneNumbers;
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

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetEmployees(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            var employee = await _employeeRepository.GetEmployeeByIdAsync(userId);

            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<AccountDto?> GetUserById(int id)
        {
            var user = await _employeeRepository.GetUserById(id);
            return user == null ? null : _mapper.Map<AccountDto>(user);
        }

        public async Task<EmployeeEnum> UpdateEmployee(string userId, EmployeeDto employeeDto)
        {
            if (string.IsNullOrWhiteSpace(userId) || employeeDto == null)
                return EmployeeEnum.NotFound;

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(userId);

            if (existingEmployee == null)
                return EmployeeEnum.NotFound;

            if (employeeDto.Phone != null)
            {
                if (!IsPhoneNumberValid(employeeDto.Phone, "VN"))
                {
                    return EmployeeEnum.InvalidPhoneNumber;
                }
            }

            _mapper.Map(employeeDto, existingEmployee);

            var updateSuccess = await _employeeRepository.UpdateEmployeeAsync(existingEmployee);

            return updateSuccess ? EmployeeEnum.Success : EmployeeEnum.UpdateFailed;
        }

        public async Task<string?> GetEmployeeEmail(string userId)
        {
            return await _employeeRepository.GetEmployeeEmail(userId);
        }

        private bool IsPhoneNumberValid(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedNumber = phoneNumberUtil.Parse(phoneNumber, regionCode);
                return phoneNumberUtil.IsValidNumber(parsedNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}

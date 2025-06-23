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

        // Lấy danh sách tất cả nhân viên
        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees); // Sử dụng AutoMapper
        }

        // Lấy thông tin một nhân viên theo id
        public async Task<EmployeeDto?> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDto>(employee); // Sử dụng AutoMapper
        }

        // Tạo mới nhân viên
        public async Task<EmployeeDto> PostEmployee(EmployeeDto employeeDto)
        {
            // Ánh xạ từ EmployeeDto sang Employee
            var employee = _mapper.Map<Employee>(employeeDto);

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

            // Ánh xạ từ Employee sang EmployeeDto để trả về
            return _mapper.Map<EmployeeDto>(createdEmployee);
        }

        // Cập nhật thông tin nhân viên
        public async Task<bool> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
            {
                return false;
            }

            var employee = _mapper.Map<Employee>(employeeDto); // Ánh xạ từ EmployeeDto sang Employee

            var updated = await _employeeRepository.UpdateEmployeeAsync(employee);

            return updated; // Trả về kết quả cập nhật
        }

        // Xóa nhân viên
        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id); // Trả về kết quả xóa
        }
    }
}

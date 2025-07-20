using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using DiamondAssessmentSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICertificateRepository _certificateRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository resultRepository, IMapper mapper, ICertificateRepository certificateRepository, ICurrentUserService currentUser, IEmployeeRepository employeeRepository, UserManager<User> userManager)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
            _certificateRepository = certificateRepository;
            _currentUser = currentUser;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
        }

        // Lấy danh sách kết quả
        public async Task<IEnumerable<ResultDto>> GetResultsAsync()
        {
            var results = await _resultRepository.GetResultsAsync();
            var resultDtos = _mapper.Map<IEnumerable<ResultDto>>(results);

            foreach (var dto in resultDtos)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(dto.EmployeeId);
                if (employee != null)
                {
                    var user = await _userManager.FindByIdAsync(employee.UserId);
                    if (user != null)
                    {
                        dto.EmployeeName = $"{user.FirstName} {user.LastName}";
                    }
                }
            }

            return resultDtos;
        }

        public async Task<IEnumerable<ResultDto>> GetResultsByDiamondIdAsync(int diamondId)
        {
            var results = await _resultRepository.GetResultsByDiamondIdAsync(diamondId);
            var resultDtos = _mapper.Map<IEnumerable<ResultDto>>(results);

            foreach (var dto in resultDtos)
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(dto.EmployeeId);
                if (employee != null)
                {
                    var user = await _userManager.FindByIdAsync(employee.UserId);
                    if (user != null)
                    {
                        dto.EmployeeName = $"{user.FirstName} {user.LastName}";
                    }
                }
            }

            return resultDtos;
        }

        private async Task<int> GenerateNextDiamondIdAsync()
        {
            var lastResult = await _resultRepository.GetLastResultAsync();
            return (lastResult?.DiamondId ?? 0) + 1;
        }

        public async Task<IEnumerable<ResultDto>> GetPersonalResults(string userId)
        {
            var results = await _resultRepository.GetPersonalResults(userId);
            return _mapper.Map<IEnumerable<ResultDto>>(results); 
        }

        // Lấy kết quả theo ID
        public async Task<ResultDto> GetResultByIdAsync(int id)
        {
            var result = await _resultRepository.GetResultByIdAsync(id);
            if (result == null)
            {
                return null; 
            }

            return _mapper.Map<ResultDto>(result);
        }

        // Tạo kết quả mới
        public async Task<ResultDto> CreateResultAsync(ResultCreateDto resultCreateDto)
        {
            var result = _mapper.Map<Result>(resultCreateDto);

            // Gán EmployeeId là Assessor hiện tại nếu chưa có
            if (result.EmployeeId == 0 || result.EmployeeId == null)
            {
                result.EmployeeId = _currentUser.EmployeeId ?? 0;
            }

            //tự tạo diamond id
            result.DiamondId = await GenerateNextDiamondIdAsync();

            var createdResult = await _resultRepository.CreateResultAsync(result);
            var resultDto = _mapper.Map<ResultDto>(createdResult);

            // Lấy thông tin assessor từ _currentUser, trả về trong DTO tạm thời
            resultDto.EmployeeId = _currentUser.EmployeeId ?? 0;

            return resultDto;
        }

        // Cập nhật kết quả
        public async Task<bool> UpdateResultAsync(int id, ResultCreateDto resultCreateDto)
        {
            var existingResult = await _resultRepository.GetResultByIdAsync(id);

            if (existingResult == null)
            {
                return false; 
            }

            _mapper.Map(resultCreateDto, existingResult);  

            if (existingResult.Status == "Completed")
            {
                var cer = new Certificate
                {
                    IssueDate = DateTime.Now,
                    ResultId = id,
                    Status = "Pending"
                };

                await _certificateRepository.CreateCertificateAsync(cer);
            }

            return await _resultRepository.UpdateResultAsync(existingResult); 
        }

        // Xóa kết quả
        public async Task<bool> DeleteResultAsync(int id)
        {
            var existingResult = await _resultRepository.GetResultByIdAsync(id);

            if (existingResult == null)
            {
                return false;
            }

            existingResult.Status = "InActive";
            return await _resultRepository.DeleteResultAsync(id);
        }
    }
}

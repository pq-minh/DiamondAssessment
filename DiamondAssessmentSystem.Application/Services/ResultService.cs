using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;

        public ResultService(IResultRepository resultRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
        }

        // Lấy danh sách kết quả
        public async Task<IEnumerable<ResultDto>> GetResultsAsync()
        {
            var results = await _resultRepository.GetResultsAsync();
            return _mapper.Map<IEnumerable<ResultDto>>(results);  
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
                return null;  // Nếu không tìm thấy, trả về null
            }

            return _mapper.Map<ResultDto>(result);  // Map từ entity sang DTO
        }

        // Tạo kết quả mới
        public async Task<ResultDto> CreateResultAsync(ResultCreateDto resultCreateDto)
        {
            var result = _mapper.Map<Result>(resultCreateDto);  

            var createdResult = await _resultRepository.CreateResultAsync(result);
            return _mapper.Map<ResultDto>(createdResult);
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
            return await _resultRepository.UpdateResultAsync(existingResult);
        }
    }
}

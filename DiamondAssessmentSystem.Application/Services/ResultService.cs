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
            return _mapper.Map<IEnumerable<ResultDto>>(results);  // Map từ entity sang DTO
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
            var result = _mapper.Map<Result>(resultCreateDto);  // Map từ ResultCreateDto sang entity

            var createdResult = await _resultRepository.CreateResultAsync(result);
            return _mapper.Map<ResultDto>(createdResult);  // Map từ entity sang DTO
        }

        // Cập nhật kết quả
        public async Task<bool> UpdateResultAsync(int id, ResultCreateDto resultCreateDto)
        {
            var existingResult = await _resultRepository.GetResultByIdAsync(id);
            if (existingResult == null)
            {
                return false;  // Không tìm thấy kết quả cần cập nhật
            }

            _mapper.Map(resultCreateDto, existingResult);  // Map từ DTO vào entity hiện tại

            return await _resultRepository.UpdateResultAsync(existingResult);  // Cập nhật kết quả trong DB
        }

        // Xóa kết quả
        public async Task<bool> DeleteResultAsync(int id)
        {
            return await _resultRepository.DeleteResultAsync(id);  // Xóa kết quả theo ID
        }
    }
}

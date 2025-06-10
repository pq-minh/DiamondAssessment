using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        // Lấy danh sách tất cả form
        public async Task<IEnumerable<RequestDto>> GetFormsAsync()
        {
            var forms = await _requestRepository.GetRequestsAsync();  // Sử dụng phương thức của IRequestRepository
            return _mapper.Map<IEnumerable<RequestDto>>(forms); // Sử dụng AutoMapper để map từ entity sang DTO
        }

        // Lấy form theo ID
        public async Task<RequestDto> GetFormByIdAsync(int id)
        {
            var form = await _requestRepository.GetRequestByIdAsync(id); // Sử dụng phương thức của IRequestRepository
            if (form == null)
            {
                return null;  // Nếu không tìm thấy form, trả về null
            }

            return _mapper.Map<RequestDto>(form); // Sử dụng AutoMapper để map từ entity sang DTO
        }

        // Tạo một form mới
        public async Task<RequestDto> CreateFormAsync(RequestCreateDto formCreateDto)
        {
            var form = _mapper.Map<Request>(formCreateDto);  // Map từ FormCreateDto sang entity Form

            var createdForm = await _requestRepository.CreateRequestAsync(form); // Sử dụng phương thức của IRequestRepository
            return _mapper.Map<RequestDto>(createdForm); // Map từ entity sang DTO
        }

        // Cập nhật form
        public async Task<bool> UpdateFormAsync(int id, RequestCreateDto formCreateDto)
        {
            var existingForm = await _requestRepository.GetRequestByIdAsync(id); // Sử dụng phương thức của IRequestRepository
            if (existingForm == null)
            {
                return false;  // Nếu không tìm thấy form, trả về false
            }

            // Cập nhật thông tin cho form hiện tại
            _mapper.Map(formCreateDto, existingForm);  // Map từ DTO vào entity hiện tại

            return await _requestRepository.UpdateRequestAsync(existingForm); // Cập nhật form trong DB
        }

        // Xóa form theo ID
        public async Task<bool> DeleteFormAsync(int id)
        {
            return await _requestRepository.DeleteRequestAsync(id); // Xóa form trong DB
        }
    }
}

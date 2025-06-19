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
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository requestRepository, ICurrentUserService currentUser, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        // Lấy danh sách tất cả các yêu cầu
        public async Task<IEnumerable<RequestDto>> GetFormsAsync()
        {
            var forms = await _requestRepository.GetRequestsAsync();
            return _mapper.Map<IEnumerable<RequestDto>>(forms);
        }

        // Lấy thông tin chi tiết một yêu cầu theo ID
        public async Task<RequestDto> GetFormByIdAsync(int id)
        {
            var form = await _requestRepository.GetRequestByIdAsync(id);
            if (form == null) return null;
            return _mapper.Map<RequestDto>(form);
        }

        // Lấy yêu cầu theo CustomerId (lịch sử của người dùng)
        public async Task<IEnumerable<RequestDto>> GetRequestsByCustomerIdAsync(int customerId)
        {
            var requests = await _requestRepository.GetRequestsByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        // Tạo một bản nháp yêu cầu mới
        public async Task<RequestDto> CreateDraftRequestAsync(RequestCreateDto draftDto)
        {
            var draft = _mapper.Map<Request>(draftDto);
            draft.Status = "Draft"; // đảm bảo trạng thái là bản nháp

            var created = await _requestRepository.CreateDraftRequestAsync(draft);
            return _mapper.Map<RequestDto>(created);
        }

        // Hủy yêu cầu nếu nó là bản nháp
        public async Task<bool> CancelRequestAsync(int requestId)
        {
            return await _requestRepository.CancelRequestAsync(requestId);
        }

        // Tạo yêu cầu chính thức
        public async Task<RequestDto> CreateFormAsync(RequestCreateDto formCreateDto)
        {
            var form = _mapper.Map<Request>(formCreateDto);
            form.Status = "Pending"; // đảm bảo trạng thái mặc định
            var created = await _requestRepository.CreateRequestAsync(form);
            return _mapper.Map<RequestDto>(created);
        }

        // Cập nhật yêu cầu
        public async Task<bool> UpdateFormAsync(int id, RequestCreateDto formCreateDto)
        {
            var existingForm = await _requestRepository.GetRequestByIdAsync(id);
            if (existingForm == null) return false;

            _mapper.Map(formCreateDto, existingForm);
            return await _requestRepository.UpdateRequestAsync(existingForm);
        }

        // Xóa yêu cầu
        public async Task<bool> DeleteFormAsync(int id)
        {
            return await _requestRepository.DeleteRequestAsync(id);
        }
    }
}

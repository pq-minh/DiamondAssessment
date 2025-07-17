using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly DiamondAssessmentDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public RequestService(IRequestRepository requestRepository, ICurrentUserService currentUser, IMapper mapper, DiamondAssessmentDbContext context)
        {
            _requestRepository = requestRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }

        // Lấy danh sách tất cả các yêu cầu
        public async Task<IEnumerable<RequestDto>> GetAllAsync()
        {
            var forms = await _requestRepository.GetRequestsAsync();
            return _mapper.Map<IEnumerable<RequestDto>>(forms);
        }

        // Lấy thông tin chi tiết một yêu cầu theo ID
        public async Task<RequestDto> GetRequestByIdAsync(int id)
        {
            var form = await _requestRepository.GetRequestByIdAsync(id);
            if (form == null) return null;
            return _mapper.Map<RequestDto>(form);
        }

        // Lấy yêu cầu theo CustomerId (lịch sử của người dùng)
        public async Task<IEnumerable<RequestDto>> GetRequestsByCustomerIdAsync(string userId)
        {
            var requests = await _requestRepository.GetRequestsByCustomerIdAsync(userId);
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        // Tạo một bản nháp yêu cầu mới
        public async Task<bool> CreateDraftRequestAsync(string userId, CreateRequestDto draftDto)
        {
            var draft = _mapper.Map<Request>(draftDto);
            draft.Status = "Draft";

            var res = await _requestRepository.CreateDraftRequest(userId, draft);
 
            return res;
        }

        // Hủy yêu cầu nếu nó là bản nháp
        public async Task<bool> CancelRequest(string userId, int requestId)
        {
            return await _requestRepository.CancelRequestAsync(userId, requestId);
        }

        // Tạo yêu cầu chính thức
        public async Task<RequestDto> CreateRequestAsync(CreateRequestDto createDto)
        {
            // Kiểm tra xem customer có tồn tại không
            var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == createDto.CustomerId);
            if (!customerExists)
                throw new Exception("Customer not found");

            // Kiểm tra xem nhân viên có tồn tại và có vai trò là Consultant không
            if (createDto.EmployeeId.HasValue)
            {
                var consultant = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == createDto.EmployeeId.Value);
                //.FirstOrDefaultAsync(e => e.EmployeeId == createDto.EmployeeId.Value && e.Role == "Consultant");

                if (consultant == null)
                    throw new Exception("Consultant not found or invalid role");
            }

            var request  = _mapper.Map<Request>(createDto);
            request.Status = "Pending";

            var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
            request.RequestDate = vietnamTime;

            var created = await _requestRepository.AddAsync(request);
            return _mapper.Map<RequestDto>(created);
        }

        // Cập nhật yêu cầu
        public async Task<bool> UpdateFormAsync(int id, CreateRequestDto formCreateDto)
        {
            var existingForm = await _requestRepository.GetRequestByIdAsync(id);
            if (existingForm == null) return false;

            _mapper.Map(formCreateDto, existingForm);
            return await _requestRepository.UpdateRequestAsync(existingForm);
        }

        public async Task<bool> DeleteRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
                return false;

            return await _requestRepository.DeleteAsync(request);
        }

        public async Task<bool> CancelRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);

            if (request == null)
                return false;

            if (request.Status == "Completed" || request.Status == "Cancel")
                return false;

            request.Status = "Cancel";

            return await _requestRepository.UpdateStatusAsync(request);
        }

    }
}

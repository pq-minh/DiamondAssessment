using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using DiamondAssessmentSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ICustomerRepository _customerRepository;
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

        // Tạo yêu cầu chính thức
        public async Task<RequestDto> CreateRequestAsync(CreateRequestDto createDto)
        {
            var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == createDto.CustomerId);
            if (!customerExists)
                throw new Exception("Customer not found");

            Employee? consultant = null;
            User? consultantUser = null;

            if (createDto.EmployeeId.HasValue)
            {
                consultant = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeId == createDto.EmployeeId.Value);

                if (consultant == null)
                    throw new Exception("Consultant not found");

                consultantUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == consultant.UserId);
            }

            var request = _mapper.Map<Request>(createDto);
            request.Status = "Pending";

            var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            request.RequestDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

            var created = await _requestRepository.AddAsync(request);

            var result = _mapper.Map<RequestDto>(created);

            // Ghép tên nếu có consultant
            if (consultantUser != null)
            {
                result.EmployeeName = $"{consultantUser.FirstName} {consultantUser.LastName}".Trim();
            }

            return result;
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
        // Draft----------------------------------------------------------------

        // Tạo một bản nháp yêu cầu mới
        public async Task<RequestDto> CreateDraftRequestAsync(string? userId, CreateDraftRequestDto draftDto)
        {
            int customerId;

            if (!string.IsNullOrEmpty(userId))
            {
                var customerIdFromRepo = await _customerRepository.GetCustomerIdByUserIdAsync(userId);
                if (customerIdFromRepo == null)
                    throw new InvalidOperationException("Customer not found.");

                customerId = customerIdFromRepo.Value;
            }
            else
            {
                if (draftDto.CustomerId <= 0)
                    throw new InvalidOperationException("CustomerId is required if user is not authenticated.");

                customerId = draftDto.CustomerId;
            }

            var request = _mapper.Map<Request>(draftDto);
            request.CustomerId = customerId;
            request.Status = "Draft";
            request.RequestDate = DateTime.UtcNow;

            var created = await _requestRepository.AddDraftAsync(request);
            return _mapper.Map<RequestDto>(created);
        }

        // Hủy yêu cầu nếu nó là bản nháp
        public async Task<bool> CancelRequest(string userId, int requestId)
        {
            return await _requestRepository.CancelRequestAsync(userId, requestId);
        }

        public async Task<bool> SubmitRequestAsync(int requestId)
        {
            var request = await _requestRepository.GetRequestByIdAsync(requestId);
            if (request == null) return false;

            if (request.Status != "Draft") return false;

            request.Status = "Pending"; // Hoặc "Submitted"
            return await _requestRepository.UpdateStatusAsync(request);
        }
    }
}

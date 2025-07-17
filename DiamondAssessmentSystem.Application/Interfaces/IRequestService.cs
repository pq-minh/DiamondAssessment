using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestDto>> GetAllAsync();
        Task<RequestDto> GetRequestByIdAsync(int id);
        Task<IEnumerable<RequestDto>> GetRequestsByCustomerIdAsync(string userId);
        Task<RequestDto> CreateRequestAsync(CreateRequestDto createDto);
        Task<bool> UpdateFormAsync(int id, CreateRequestDto formCreateDto);
        Task<bool> DeleteRequestAsync(int requestId);
        Task<bool> CancelRequestAsync(int requestId);

        //draft-------------------------------------------------------
        Task<bool> CreateDraftRequestAsync(string userId, CreateRequestDto draftDto);
        Task<bool> CancelRequest(string userId, int requestId);
    }
}

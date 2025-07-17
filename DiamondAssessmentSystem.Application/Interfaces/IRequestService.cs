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
        Task<IEnumerable<RequestDto>> GetFormsAsync();
        Task<RequestDto> GetFormByIdAsync(int id);
        Task<IEnumerable<RequestDto>> GetRequestsByCustomerIdAsync(string userId);
        Task<RequestDto> CreateFormAsync(CreateRequestDto formCreateDto);
        Task<bool> CreateDraftRequestAsync(string userId, CreateRequestDto draftDto);
        Task<bool> CancelRequest(string userId, int requestId);
        Task<bool> UpdateFormAsync(int id, CreateRequestDto formCreateDto);
    }
}

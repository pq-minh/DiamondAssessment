using DiamondAssessmentSystem.Application.DTO;
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
        Task<RequestDto> CreateFormAsync(RequestCreateDto formCreateDto);
        Task<bool> CreateDraftRequestAsync(string userId, RequestCreateDto draftDto);
        Task<bool> CancelRequest(string userId, int requestId);
        Task<bool> UpdateFormAsync(int id, RequestCreateDto formCreateDto);
    }
}

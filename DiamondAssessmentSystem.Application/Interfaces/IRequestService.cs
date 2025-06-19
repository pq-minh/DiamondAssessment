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
        Task<IEnumerable<RequestDto>> GetRequestsByCustomerIdAsync(int customerId);
        Task<RequestDto> CreateFormAsync(RequestCreateDto formCreateDto);
        Task<RequestDto> CreateDraftRequestAsync(RequestCreateDto draftDto);
        Task<bool> CancelRequestAsync(int requestId);
        Task<bool> UpdateFormAsync(int id, RequestCreateDto formCreateDto);
        Task<bool> DeleteFormAsync(int id);
    }
}

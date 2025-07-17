using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetRequestsAsync();
        Task<Request?> GetRequestByIdAsync(int id);
        Task<IEnumerable<Request>> GetRequestsByCustomerIdAsync(string userId);
        Task<Request> AddAsync(Request request);
        Task<bool> UpdateRequestAsync(Request request);
        Task<bool> DeleteAsync(Request request);
        Task<bool> UpdateStatusAsync(Request request);
        //draft----------------------------------------
        Task<bool> CreateDraftRequest(string userId, Request request);
        Task<bool> CancelRequestAsync(string userId, int requestId);

    }
}

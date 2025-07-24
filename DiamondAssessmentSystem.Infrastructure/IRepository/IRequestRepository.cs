using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetRequestsAsync();

        Task<Request?> GetRequestByIdAsync(int id);

        Task<IEnumerable<Request>> GetRequestsByCustomerAsync(string userId);

        Task<IEnumerable<Request>> GetRequestsByCustomerIdAsync(string userId);

        Task<List<Request>> GetDraftOrPendingRequestsAsync(string userId);

        Task<bool> CreateDraftRequest(string userId, Request request);

        Task<bool> CancelRequestAsync(string userId, int requestId);

        Task<Request> CreateRequestAsync(Request request);

        Task<bool> UpdateRequestAsync(Request request);

        Task<int> GetEmployeeId(string userId);

    }
}

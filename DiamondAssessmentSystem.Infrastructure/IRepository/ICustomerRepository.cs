using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(string userId);
        Task<Customer?> GetCustomerByUserIdAsync(string userId);
        Task<int?> GetCustomerIdByUserIdAsync(string userId);
        Task<Customer?> GetCustomerByCustomerIdAsync(int customerId);
        Task<bool> UpdateCustomerAsync(Customer customer);
    }
}

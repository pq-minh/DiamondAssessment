using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto);
        Task<bool> UpdateCustomerAsync(int id, CustomerCreateDto customerCreateDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}

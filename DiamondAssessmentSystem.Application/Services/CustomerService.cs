using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: api/Customer
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers); // Sử dụng AutoMapper để map từ entity sang DTO
        }

        // GET: api/Customer/5
        public async Task<CustomerDto> GetCustomerByIdAsync(string userId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(userId);
            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerDto>(customer); 
        }

        // PUT: api/Customer/5
        public async Task<bool> UpdateCustomerAsync(string userId, CustomerCreateDto customerCreateDto)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(userId);
            if (existingCustomer == null)
            {
                return false;  
            }

            _mapper.Map(customerCreateDto, existingCustomer);  

            return await _customerRepository.UpdateCustomerAsync(existingCustomer); 
        }
    }
}

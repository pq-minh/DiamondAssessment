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
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return null;  // Nếu không tìm thấy customer, trả về null
            }

            return _mapper.Map<CustomerDto>(customer); // Sử dụng AutoMapper để map từ entity sang DTO
        }

        // POST: api/Customer
        public async Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Customer>(customerCreateDto);  // Map từ CustomerCreateDto sang entity Customer

            var createdCustomer = await _customerRepository.CreateCustomerAsync(customer);
            return _mapper.Map<CustomerDto>(createdCustomer); // Map từ entity sang DTO
        }

        // PUT: api/Customer/5
        public async Task<bool> UpdateCustomerAsync(int id, CustomerCreateDto customerCreateDto)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                return false;  // Nếu không tìm thấy customer, trả về false
            }

            // Cập nhật thông tin cho customer hiện tại
            _mapper.Map(customerCreateDto, existingCustomer);  // Map từ DTO vào entity hiện tại

            return await _customerRepository.UpdateCustomerAsync(existingCustomer); // Cập nhật customer trong DB
        }

        // DELETE: api/Customer/5
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteCustomerAsync(id); // Xóa customer trong DB
        }
    }
}

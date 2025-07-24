using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Enums;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using PhoneNumbers;
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

        public async Task<CustomerDto> GetCustomerByIdAsync(string userId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(userId);
            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerDto>(customer); 
        }

        public async Task<UpdateCustomerResult> UpdateCustomerAsync(string userId, CustomerCreateDto customerCreateDto)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(userId);
            if (existingCustomer == null)
            {
                return UpdateCustomerResult.CustomerNotFound;
            }

            if (!IsPhoneNumberValid(customerCreateDto.Phone, "VN"))
            {
                return UpdateCustomerResult.InvalidPhoneNumber;
            }

            _mapper.Map(customerCreateDto, existingCustomer);

            var updateSuccess = await _customerRepository.UpdateCustomerAsync(existingCustomer);
            return updateSuccess ? UpdateCustomerResult.Success : UpdateCustomerResult.UpdateFailed;
        }


        private bool IsPhoneNumberValid(string phoneNumber, string regionCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var parsedNumber = phoneNumberUtil.Parse(phoneNumber, regionCode);
                return phoneNumberUtil.IsValidNumber(parsedNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}

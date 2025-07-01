using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Application.Services
{
    public class ServicePriceService : IServicePriceService
    {
        private readonly IMapper _mapper;
        private readonly IServicePriceRepository _servicePriceRepository;

        public ServicePriceService(IServicePriceRepository servicePriceRepository, IMapper mapper)
        {
            _servicePriceRepository = servicePriceRepository;
            _mapper = mapper;
        }

        // GET: api/ServicePrices
        public async Task<IEnumerable<ServicePriceDto>> GetServicePrices()
        {
            var servicePrices = await _servicePriceRepository.GetServicePricesAsync();
            var result = _mapper.Map<IEnumerable<ServicePriceDto>>(servicePrices);
            return result;
        }

        public async Task<IEnumerable<ServicePriceDto>> GetServicePrices(string status)
        {
            var servicePrices = await _servicePriceRepository.GetServicePrices(status);
            var result = _mapper.Map<IEnumerable<ServicePriceDto>>(servicePrices);
            return result;
        }

        // GET: api/ServicePrices/{id}
        public async Task<ServicePriceDto?> GetServicePrice(int id)
        {
            var servicePrice = await _servicePriceRepository.GetServicePriceByIdAsync(id);

            if (servicePrice == null)
            {
                return null;
            }

            var result = _mapper.Map<ServicePriceDto>(servicePrice);
            return result;
        }

        // POST: api/ServicePrices
        public async Task<ServicePriceDto> PostServicePrice(ServicePriceCreateDto servicePriceCreateDto)
        {
            var servicePrice = _mapper.Map<ServicePrice>(servicePriceCreateDto);

            var createdServicePrice = await _servicePriceRepository.CreateServicePriceAsync(servicePrice);

            var createdServicePriceDto = _mapper.Map<ServicePriceDto>(createdServicePrice);
            return createdServicePriceDto;
        }

        // PUT: api/ServicePrices/{id}
        public async Task<bool> UpdateServicePrice(int id, ServicePriceCreateDto servicePriceCreateDto)
        {
            var existingServicePrice = await _servicePriceRepository.GetServicePriceByIdAsync(id);

            if (existingServicePrice == null)
            {
                return false;
            }

            // Dùng AutoMapper để ánh xạ từ DTO vào Entity (tính đến trường hợp cần cập nhật chỉ một số trường)
            existingServicePrice = _mapper.Map(servicePriceCreateDto, existingServicePrice);

            var updated = await _servicePriceRepository.UpdateServicePriceAsync(existingServicePrice);

            return updated;
        }

        // DELETE: api/ServicePrices/{id}
        public async Task<bool> DeleteServicePrice(int id)
        {
            var existingServicePrice = await _servicePriceRepository.GetServicePriceByIdAsync(id);

            if (existingServicePrice == null)
            {
                return false;
            }

            existingServicePrice.Status = "InActive";

            var deleted = await _servicePriceRepository.UpdateServicePriceAsync(existingServicePrice);
            return deleted;
        }
    }
}

using DiamondAssessmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Interfaces
{
    public interface IServicePriceService
    {
        Task<IEnumerable<ServicePriceDto>> GetServicePrices();

        Task<IEnumerable<ServicePriceDto>> GetServicePrices(string status);

        Task<ServicePriceDto?> GetServicePrice(int id);

        Task<ServicePriceDto> PostServicePrice(ServicePriceCreateDto servicePriceCreateDto);

        Task<bool> UpdateServicePrice(int id, ServicePriceCreateDto servicePriceCreateDto);

        Task<bool> DeleteServicePrice(int id);
    }
}

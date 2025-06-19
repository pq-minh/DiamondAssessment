using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IServicePriceRepository
    {
        Task<IEnumerable<ServicePrice>> GetServicePricesAsync();
        Task<ServicePrice?> GetServicePriceByIdAsync(int id);
        Task<ServicePrice> CreateServicePriceAsync(ServicePrice servicePrice);
        Task<bool> UpdateServicePriceAsync(ServicePrice servicePrice);
        Task<bool> DeleteServicePriceAsync(int id);
    }
}

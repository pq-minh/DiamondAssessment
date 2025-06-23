using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IServicePriceRepository
    {
        Task<IEnumerable<ServicePrice>> GetServicePricesAsync();
        Task<IEnumerable<ServicePrice>> GetServicePrices(string status);
        Task<ServicePrice?> GetServicePriceByIdAsync(int id);
        Task<ServicePrice> CreateServicePriceAsync(ServicePrice servicePrice);
        Task<bool> UpdateServicePriceAsync(ServicePrice servicePrice);
    }
}

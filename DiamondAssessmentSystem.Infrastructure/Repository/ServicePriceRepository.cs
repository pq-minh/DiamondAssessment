using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class ServicePriceRepository : IServicePriceRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public ServicePriceRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicePrice>> GetServicePricesAsync()
        {
            return await _context.ServicePrices.ToListAsync();
        }

        public async Task<ServicePrice?> GetServicePriceByIdAsync(int id)
        {
            return await _context.ServicePrices.FindAsync(id);
        }

        public async Task<ServicePrice> CreateServicePriceAsync(ServicePrice servicePrice)
        {
            _context.ServicePrices.Add(servicePrice);
            await _context.SaveChangesAsync();
            return servicePrice;
        }

        public async Task<bool> UpdateServicePriceAsync(ServicePrice servicePrice)
        {
            _context.Entry(servicePrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServicePriceExistsAsync(servicePrice.ServiceId)) 
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteServicePriceAsync(int id)
        {
            var servicePrice = await _context.ServicePrices.FindAsync(id);
            if (servicePrice == null)
            {
                return false;
            }

            _context.ServicePrices.Remove(servicePrice);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ServicePriceExistsAsync(int id)
        {
            return await _context.ServicePrices.AnyAsync(e => e.ServiceId == id); 
        }
    }
}

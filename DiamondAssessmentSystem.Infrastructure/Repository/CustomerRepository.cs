using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public CustomerRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers
                                 .Include(c => c.ChatLogs)      // Bao gồm mối quan hệ với ChatLogs
                                 .Include(c => c.Orders)        // Bao gồm mối quan hệ với Orders
                                 .Include(c => c.Requests)      // Bao gồm mối quan hệ với Requests
                                 .ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                                 .Include(c => c.ChatLogs)      // Bao gồm mối quan hệ với ChatLogs
                                 .Include(c => c.Orders)        // Bao gồm mối quan hệ với Orders
                                 .Include(c => c.Requests)      // Bao gồm mối quan hệ với Requests
                                 .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExistsAsync(customer.CustomerId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CustomerExistsAsync(int id)
        {
            return await _context.Customers.AnyAsync(e => e.CustomerId == id);
        }
    }
}

using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public PaymentRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdatePayment(string userId, string? status, string? method)
        {
            var customerId = await GetCustomerId(userId);

            if (status == null || customerId <= 0)
            {
                return false;
            }

            var order = await _context.Orders.Where(od => od.CustomerId == customerId).OrderByDescending(od => od.OrderDate).FirstOrDefaultAsync();

            if (order == null)
            {
                return false;
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == order.OrderId);

            if (payment == null)
            {
                return false;
            }

            payment.Status = status;
            payment.Method = method;

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<int> GetCustomerId(string userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (customer == null)
            {
                return -1;
            }

            return customer.CustomerId;
        }
    }
}

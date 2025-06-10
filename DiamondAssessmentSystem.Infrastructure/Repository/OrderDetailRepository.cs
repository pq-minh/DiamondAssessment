using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly DiamondAssessmentDbContext _context;
        private readonly ILogger<OrderDetailRepository> _logger;

        public OrderDetailRepository(DiamondAssessmentDbContext context, ILogger<OrderDetailRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync()
        {
            try
            {
                return await _context.OrderDetails
                    .Include(od => od.Service)  // Bao gồm thông tin giá dịch vụ
                    .Include(od => od.Result)        // Bao gồm thông tin kết quả đánh giá
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching order details: {ex.Message}");
                throw;
            }
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            try
            {
                return await _context.OrderDetails
                    .Include(od => od.Service)
                    .Include(od => od.Result)
                    .FirstOrDefaultAsync(od => od.OrderDetailId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching order detail with id {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();
                return orderDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating order detail: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                _context.Entry(orderDetail).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderDetailExistsAsync(orderDetail.OrderDetailId))
                {
                    _logger.LogWarning($"OrderDetail with id {orderDetail.OrderDetailId} not found during update.");
                    return false;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating order detail: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            try
            {
                var orderDetail = await _context.OrderDetails.FindAsync(id);
                if (orderDetail == null)
                {
                    _logger.LogWarning($"OrderDetail with id {id} not found for deletion.");
                    return false;
                }

                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting order detail with id {id}: {ex.Message}");
                throw;
            }
        }

        private async Task<bool> OrderDetailExistsAsync(int id)
        {
            return await _context.OrderDetails.AnyAsync(e => e.OrderDetailId == id);
        }
    }
}

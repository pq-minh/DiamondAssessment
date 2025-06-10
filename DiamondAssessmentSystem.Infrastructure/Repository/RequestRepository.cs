using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class RequestRepository : IRequestRepository // Đổi tên thành RequestRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public RequestRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        // Thay vì lấy form, bạn lấy các yêu cầu dịch vụ (Request)
        public async Task<IEnumerable<Request>> GetRequestsAsync()
        {
            return await _context.Requests
                .Include(r => r.Customer)              // Liên kết với Customer (Khách hàng)
                .Include(r => r.Employee)              // Liên kết với Employee (Nhân viên)
                .Include(r => r.Receipts)              // Liên kết với Receipts (Biên nhận)
                .Include(r => r.CommitmentRecords)     // Liên kết với CommitmentRecords (Cam kết)
                .Include(r => r.SealingRecords)        // Liên kết với SealingRecords (Niêm phong)
                .ToListAsync();
        }

        // Lấy thông tin chi tiết một yêu cầu theo ID
        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _context.Requests
                .Include(r => r.Customer)
                .Include(r => r.Employee)
                .Include(r => r.Receipts)
                .Include(r => r.CommitmentRecords)
                .Include(r => r.SealingRecords)
                .FirstOrDefaultAsync(r => r.RequestId == id);
        }

        // Tạo một yêu cầu mới (thay vì form, bạn thêm yêu cầu)
        public async Task<Request> CreateRequestAsync(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        // Cập nhật thông tin yêu cầu
        public async Task<bool> UpdateRequestAsync(Request request)
        {
            _context.Entry(request).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(request.RequestId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        // Xóa một yêu cầu
        public async Task<bool> DeleteRequestAsync(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return false;
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }

        // Kiểm tra xem yêu cầu có tồn tại hay không
        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}

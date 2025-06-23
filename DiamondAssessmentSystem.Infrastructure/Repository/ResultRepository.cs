using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public ResultRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetResultsAsync()
        {
            return await _context.Results.Include(r => r.Request)   // Lấy thông tin Request, từ đó sẽ có thông tin nhân viên
                                         .ThenInclude(r => r.Employee) // Thêm nhân viên từ mối quan hệ với Request
                                         .Include(r => r.Certificates)
                                         .ToListAsync();
        }

        public async Task<Result> GetResultByIdAsync(int id)
        {
            return await _context.Results.Include(r => r.Request)   // Lấy thông tin Request, từ đó sẽ có thông tin nhân viên
                                         .ThenInclude(r => r.Employee) // Thêm nhân viên từ mối quan hệ với Request
                                         .Include(r => r.Certificates)
                                         .FirstOrDefaultAsync(r => r.ResultId == id);
        }

        public async Task<Result> CreateResultAsync(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateResultAsync(Result result)
        {
            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ResultExistsAsync(result.ResultId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteResultAsync(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return false;
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ResultExistsAsync(int id)
        {
            return await _context.Results.AnyAsync(e => e.ResultId == id);
        }
    }
}

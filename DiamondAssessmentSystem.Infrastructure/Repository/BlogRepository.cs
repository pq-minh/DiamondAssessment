using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DiamondAssessmentDbContext _context;

        public BlogRepository(DiamondAssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs
                .Include(b => b.Employee)
                .ThenInclude(e => e.User) 
                .ToListAsync();
        }

        public async Task<Blog?> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Employee)
                .ThenInclude(e => e.User)
                .FirstOrDefaultAsync(b => b.BlogId == id);
        }

        public async Task<IEnumerable<Blog>> GetBlogsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Blogs
                .Where(b => b.EmployeeId == employeeId)
                .Include(b => b.Employee)
                .ThenInclude(e => e.User)
                .ToListAsync();
        }

        public async Task<Blog> CreateBlogAsync(string userId, Blog blog)
        {
            var employeeId = await GetEmployeeId(userId);
            blog.EmployeeId = employeeId;

            blog.CreatedDate = DateTime.UtcNow;

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<bool> UpdateBlogAsync(string userId, Blog blog)
        {
            var employeeId = await GetEmployeeId(userId);

            var existing = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blog.BlogId);
            if (existing == null)
            {
                return false;
            }

            existing.Title = blog.Title;
            existing.Content = blog.Content;
            existing.ImageUrl = blog.ImageUrl;
            existing.BlogType = blog.BlogType;
            existing.Status = blog.Status ?? "Draft";
            existing.UpdatedDate = DateTime.UtcNow;
            existing.EmployeeId = employeeId;

            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> BlogExistsAsync(int id)
        {
            return await _context.Blogs.AnyAsync(e => e.BlogId == id);
        }

        public async Task<int> GetEmployeeId(string userId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.UserId == userId);

            if (employee == null)
            {
                return -1;
            }

            return employee.EmployeeId;
        }
    }
}

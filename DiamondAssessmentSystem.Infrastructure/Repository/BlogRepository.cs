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
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<bool> UpdateBlogAsync(Blog blog)
        {
            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BlogExistsAsync(blog.BlogId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return false;
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> BlogExistsAsync(int id)
        {
            return await _context.Blogs.AnyAsync(e => e.BlogId == id);
        }
    }
}

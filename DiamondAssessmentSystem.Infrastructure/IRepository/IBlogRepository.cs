using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Infrastructure.IRepository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetBlogsAsync();
        Task<Blog?> GetBlogByIdAsync(int id);
        Task<IEnumerable<Blog>> GetBlogsByEmployeeIdAsync(int employeeId);
        Task<int> GetEmployeeId(string userId); 
        Task<Blog> CreateBlogAsync(string userId, Blog blog);
        Task<bool> UpdateBlogAsync(string userId, Blog blog);
    }
}

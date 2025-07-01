using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        // Constructor
        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        // Get all blogs
        public async Task<IEnumerable<BlogDto>> GetBlogs()
        {
            var blogs = await _blogRepository.GetBlogsAsync();
            var blogDtos = _mapper.Map<IEnumerable<BlogDto>>(blogs);  
            return blogDtos;
        }

        // Get a specific blog by Id
        public async Task<BlogDto> GetBlogById(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return null; 
            }

            var blogDto = _mapper.Map<BlogDto>(blog);  
            return blogDto;
        }

        // Create a new blog
        public async Task<BlogDto> CreateBlog(string userId, BlogDto blogDto)
        {
            // Map BlogDto to Blog entity
            var blog = _mapper.Map<Blog>(blogDto);

            var createdBlog = await _blogRepository.CreateBlogAsync(userId, blog);

            // Map the created Blog entity back to BlogDto
            var createdBlogDto = _mapper.Map<BlogDto>(createdBlog);

            return createdBlogDto;
        }

        // Update an existing blog
        public async Task<bool> UpdateBlog(string userId, BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);

            var updated = await _blogRepository.UpdateBlogAsync(userId, blog);

            return updated;
        }

        // Delete a blog
        public async Task<bool> DeleteBlog(string userId, BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);

            blog.Status = "InActive";

            var deleted = await _blogRepository.UpdateBlogAsync(userId, blog);
            return deleted;  // Return true if the blog was deleted, otherwise false
        }
    }
}

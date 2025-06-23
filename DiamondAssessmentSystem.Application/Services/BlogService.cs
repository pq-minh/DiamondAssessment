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
            var blogDtos = _mapper.Map<IEnumerable<BlogDto>>(blogs);  // Using AutoMapper to map Blog to BlogDto
            return blogDtos;
        }

        // Get a specific blog by Id
        public async Task<BlogDto> GetBlogById(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);
            if (blog == null)
            {
                return null;  // Blog not found, return null
            }

            var blogDto = _mapper.Map<BlogDto>(blog);  // Map Blog entity to BlogDto
            return blogDto;
        }

        // Create a new blog
        public async Task<BlogDto> CreateBlog(BlogDto blogDto)
        {
            // Map BlogDto to Blog entity
            var blog = _mapper.Map<Blog>(blogDto);

            // Create the blog and get the created entity
            var createdBlog = await _blogRepository.CreateBlogAsync(blog);

            // Map the created Blog entity back to BlogDto
            var createdBlogDto = _mapper.Map<BlogDto>(createdBlog);

            return createdBlogDto;
        }

        // Update an existing blog
        public async Task<bool> UpdateBlog(int id, BlogDto blogDto)
        {
            if (id != blogDto.BlogId)
            {
                return false;  // Return false if IDs do not match
            }

            // Map BlogDto to Blog entity for update
            var blog = _mapper.Map<Blog>(blogDto);

            var updated = await _blogRepository.UpdateBlogAsync(blog);

            return updated;  // Return the result of the update operation
        }

        // Delete a blog
        public async Task<bool> DeleteBlog(int id)
        {
            var deleted = await _blogRepository.DeleteBlogAsync(id);
            return deleted;  // Return true if the blog was deleted, otherwise false
        }
    }
}

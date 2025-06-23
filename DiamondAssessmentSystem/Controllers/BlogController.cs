using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/Blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogs()
        {
            var blogs = await _blogService.GetBlogs();
            return Ok(blogs);
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetBlog(int id)
        {
            var blog = await _blogService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // POST: api/Blog
        [HttpPost]
        public async Task<ActionResult<BlogDto>> CreateBlog(BlogDto blogDto)
        {
            var createdBlog = await _blogService.CreateBlog(blogDto);
            return CreatedAtAction(nameof(GetBlog), new { id = createdBlog.BlogId }, createdBlog);
        }

        // PUT: api/Blog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, BlogDto blogDto)
        {
            if (id != blogDto.BlogId)
            {
                return BadRequest();
            }

            var updated = await _blogService.UpdateBlog(id, blogDto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var deleted = await _blogService.DeleteBlog(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

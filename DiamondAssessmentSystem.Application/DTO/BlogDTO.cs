using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Application.DTO
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? BlogType { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; } 
    }
}


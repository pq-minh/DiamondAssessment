namespace DiamondAssessmentSystem.Application.DTO
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public DateOnly BlogDate { get; set; }
        public string? Description { get; set; }
        public string Context { get; set; }
        public string? Image { get; set; }
    }
}

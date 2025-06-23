namespace DiamondAssessmentSystem.Application.DTO
{
    public class ServicePriceDto
    {
        public int ServicePriceId { get; set; }
        public string ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
    }

    public class ServicePriceCreateDto
    {
        public string ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
    }
}

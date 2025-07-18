namespace DiamondAssessmentSystem.Application.DTO
{
    public class ServicePriceDto
    {
        public int ServiceId { get; set; }
        public string ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
    }

    public class ServicePriceCreateDto
    {
        public string ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        //public int EmployeeId { get; set; }
        public string Status { get; set; }
    }
}

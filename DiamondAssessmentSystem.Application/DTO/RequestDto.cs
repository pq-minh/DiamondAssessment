
using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Application.DTO
{
    public class RequestDto
    {
        public int RequestId { get; set; }
        public string FormType { get; set; }
        public DateTime RequestDate { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string? RequestType { get; set; }
        public int? EmployeeId { get; set; }
        public string? Status { get; set; }
    }

    public class RequestCreateDto
    {
        public int ServiceId { get; set; }

        public string? RequestType { get; set; }

        public int? EmployeeId { get; set; }
    }
}

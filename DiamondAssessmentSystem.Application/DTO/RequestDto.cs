
using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Application.DTO
{
    public class RequestDto
    {
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        //public string ServiceName { get; set; }
        public string? RequestType { get; set; }
        public int? EmployeeId { get; set; } //consultant
        public string EmployeeName { get; set; } = null!;
        public string? Status { get; set; }
    }

    public class CreateRequestDto
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string RequestType { get; set; }
        public int? EmployeeId { get; set; } // Optional: nếu có thì là Consultant
    }
    public class CreateDraftRequestDto
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string RequestType { get; set; }
    }

}

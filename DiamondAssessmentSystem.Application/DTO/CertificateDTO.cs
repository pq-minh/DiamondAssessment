using System.Collections.Generic;

namespace DiamondAssessmentSystem.Application.DTO
{
    public class CertificateDto
    {
        public int CertId { get; set; }
        public DateOnly? IssueDate { get; set; }
        public List<ResultDto> Results { get; set; } = new List<ResultDto>();
    }

    public class CertificateCreateDto
    {
        public DateOnly? IssueDate { get; set; }
    }
}

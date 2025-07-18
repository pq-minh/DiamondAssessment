namespace DiamondAssessmentSystem.Application.DTO
{
    public class EmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmployeeName => $"{FirstName} {LastName}".Trim();
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public decimal? Salary { get; set; }
        public string Role { get; internal set; }
        public object Status { get; internal set; }
    }

    public class UpdateEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public decimal? Salary { get; set; }
        public string? Status { get; set; }
    }
}

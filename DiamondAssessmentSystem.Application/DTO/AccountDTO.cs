namespace DiamondAssessmentSystem.Application.DTO
{
    public class AccountDto
    {
        public int Id { get; set; } // Add the Id property
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }

    public enum Role
    {
        Customer = 1,
        Admin = 2,
        Staff = 3
    }
}

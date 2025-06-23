namespace DiamondAssessmentSystem.Application.DTO
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int? AccId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? IdCard { get; set; }
        public string Address { get; set; }
        public string? UnitName { get; set; }
        public string? TaxCode { get; set; }
        public AccountDto? Acc { get; set; }
    }

    public class CustomerCreateDto
    {
        public int? AccId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? IdCard { get; set; }
        public string Address { get; set; }
        public string? UnitName { get; set; }
        public string? TaxCode { get; set; }
    }
}

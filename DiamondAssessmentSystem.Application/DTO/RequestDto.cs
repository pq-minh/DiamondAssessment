
namespace DiamondAssessmentSystem.Application.DTO
{
    public class RequestDto
    {
        public int FormId { get; set; }
        public string FormType { get; set; }
        public DateOnly CreateDate { get; set; }
        public ICollection<OrderDto> BookingCommitments { get; set; }
        public ICollection<OrderDto> BookingReceipts { get; set; }
        public ICollection<OrderDto> BookingSealings { get; set; }
    }

    public class RequestCreateDto
    {
        public int CustomerId { get; set; }
        public string FormType { get; set; }
        public DateOnly CreateDate { get; set; }
    }
}

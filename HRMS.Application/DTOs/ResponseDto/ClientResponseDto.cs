namespace HRMS.Application.DTOs.ResponseDto
{
    public class ClientResponseDto
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

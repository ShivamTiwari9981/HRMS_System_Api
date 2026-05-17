namespace HRMS.Application.DTOs.ResponseDto
{
    public class ClientUserResponseDto
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string? ProfileImagePath { get; set; }
        public bool IsCompanyProfileCreated { get; set; }
    }
}

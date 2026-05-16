namespace HRMS.Application.DTOs
{
    public class LoginResponseDto
    {
        public LoginResponseDto() { }
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsCompanyProfileCreated { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
        public LoginResponseDto(UserDto dto,string token)
        {
            UserId = dto.UserId;
            ClientId = dto.ClientId;
            //UserCode = dto.UserCode;
            UserName = dto.UserName;
            UserEmail = dto.UserEmail;
            IsCompanyProfileCreated = dto.IsCompanyProfileCreated;
            Token = token;
        }
    }
}

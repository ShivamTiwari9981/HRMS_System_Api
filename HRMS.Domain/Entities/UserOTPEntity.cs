using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    public class UserOTPEntity : BaseEntity
    {
        [Key]
        public Guid UserOtpId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public string OtpCode { get; set; }

        public DateTime ExpiryTime { get; set; }

        public bool IsUsed { get; set; } = false;
    }
}

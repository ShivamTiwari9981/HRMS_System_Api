using HRMS.Shared.Helpers;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Infrastructure.Persistence.Seeders.Constants
{
    public class BaseModel
    {
        public bool? IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedBy { get; set; } = SystemUser.DefaultSystemUser;
    }
}

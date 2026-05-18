using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    public class HRMSAppSettingEntity : BaseEntity
    {
        [Key]
        public Guid AppSettingId { get; set; } = Guid.NewGuid();

        public Guid? ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SettingKey { get; set; }

        [Required]
        [MaxLength(500)]
        public string SettingValue { get; set; }

        [MaxLength(50)]
        public string DataType { get; set; }

        public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;


namespace HRMS.Domain.Entities
{
    public abstract class BaseEntity
    {
        public bool? IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public bool? IsSynced { get; set; } = false;
    }
}

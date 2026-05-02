using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRMS.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("ClientId")]
        [Required]
        public Guid ClientId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
        [Column("CreatedBy")]
        public Guid CreatedBy { get; set; }
        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
        [Column("UpdatedBy")]
        public Guid? UpdatedBy { get; set; }
        [Column("IsSynced")]
        public bool? IsSynced { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientEntity Client { get; set; }
    }
}

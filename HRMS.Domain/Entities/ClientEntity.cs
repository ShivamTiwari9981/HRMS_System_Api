
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("Client")]
    public class ClientEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Id")]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        [Column("ClientCode")]
        public string ClientCode { get; set; }
        [Required]
        [StringLength(200)]
        [Column("CompanyName")]
        public string CompanyName { get; set; }
        [Column("CompanyLogo")]
        public string? CompanyLogo { get; set; }

        [Required]
        [StringLength(200)]
        [Column("ClientName")]
        public string ClientName { get; set; }

        [Required]
        [StringLength(200)]
        public string Domain { get; set; }

        [StringLength(200)]
        public string? ContactPerson { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(15);
        [StringLength(200)]
        public string? Address { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsSynced { get; set; }
    }
}


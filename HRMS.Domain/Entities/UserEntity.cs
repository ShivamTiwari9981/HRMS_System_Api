using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string UserCode { get; set; }
        [Required]
        [StringLength(200)]
        public string FullName { get; set; }
        [Required]
        [StringLength(200)]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string UserSalt { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public Guid RoleId { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(UserCode), IsUnique = true)]
    [Index(nameof(ClientId), nameof(UserName), IsUnique = true)]
    [Index(nameof(ClientId), nameof(UserEmail), IsUnique = true)]
    [Index(nameof(ClientId), nameof(Phone), IsUnique = true)]
    public class UserEntity : BaseEntity
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual ClientEntity Client { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserCode { get; set; }

        [MaxLength(200)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string UserSalt { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string UserEmail { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public string? ProfileImagePath { get; set; }

        [MaxLength(3)]
        public int? FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; } = null;

        public bool IsLocked { get; set; } = false;
        [Required]
        public string RoleName { get; set; }

    }
}

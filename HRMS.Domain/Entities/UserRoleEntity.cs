using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(UserId), nameof(RoleId), IsUnique = true)]
    public class UserRoleEntity:BaseEntity
    {
        [Key]
        public Guid UserRoleId { get; set; } = new Guid();
        public Guid ClientId { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleEntity Role { get; set; }
    }
}

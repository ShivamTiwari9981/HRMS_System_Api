using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class User
{
    public Guid ClientId { get; set; }

    public Guid UserId { get; set; }

    public string UserCode { get; set; }

    public string FullName { get; set; }

    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string UserSalt { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string ProfileImagePath { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

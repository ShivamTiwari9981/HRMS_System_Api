using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class UserRole
{
    public Guid ClientId { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual Role Role { get; set; }

    public virtual User User { get; set; }
}

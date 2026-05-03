using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class RolePermission
{
    public Guid ClientId { get; set; }

    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Permission Client { get; set; }

    public virtual Permission Permission { get; set; }

    public virtual Role Role { get; set; }
}

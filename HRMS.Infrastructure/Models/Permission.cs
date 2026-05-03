using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Permission
{
    public Guid ClientId { get; set; }

    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<RolePermission> RolePermissionClients { get; set; } = new List<RolePermission>();

    public virtual ICollection<RolePermission> RolePermissionPermissions { get; set; } = new List<RolePermission>();
}

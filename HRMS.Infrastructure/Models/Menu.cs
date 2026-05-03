using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Menu
{
    public Guid ClientId { get; set; }

    public int MenuId { get; set; }

    public int? ParentMenuId { get; set; }

    public string MenuName { get; set; }

    public string MenuIcon { get; set; }

    public string RouterLink { get; set; }

    public int? DisplayOrder { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<Menu> InverseParentMenu { get; set; } = new List<Menu>();

    public virtual Menu ParentMenu { get; set; }
}

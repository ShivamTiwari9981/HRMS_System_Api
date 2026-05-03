using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Department
{
    public Guid ClientId { get; set; }

    public Guid DepartmentId { get; set; }

    public string DepartmentCode { get; set; }

    public string DepartmentName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Client
{
    public Guid ClientId { get; set; }

    public string ClientCode { get; set; }

    public string ClientName { get; set; }

    public string CompanyName { get; set; }

    public string CompanyLogo { get; set; }

    public string Domain { get; set; }

    public string ContactPerson { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Gstnumber { get; set; }

    public string Address { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();

    public virtual ICollection<MasterCodeGeneration> MasterCodeGenerations { get; set; } = new List<MasterCodeGeneration>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

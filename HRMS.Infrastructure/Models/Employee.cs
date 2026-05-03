using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Employee
{
    public Guid ClientId { get; set; }

    public Guid EmployeeId { get; set; }

    public string EmployeeCode { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public Guid DepartmentId { get; set; }

    public string Designation { get; set; }

    public string ProfileImagePath { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public decimal Salary { get; set; }

    public Guid UserId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Client Client { get; set; }

    public virtual Department Department { get; set; }

    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    public virtual User User { get; set; }
}

using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Payroll
{
    public Guid ClientId { get; set; }

    public Guid PayrollId { get; set; }

    public Guid EmployeeId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal Bonus { get; set; }

    public decimal Deductions { get; set; }

    public decimal NetSalary { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual Employee Employee { get; set; }
}

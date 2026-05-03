using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class Attendance
{
    public Guid ClientId { get; set; }

    public Guid AttendanceId { get; set; }

    public string AttendanceCode { get; set; }

    public Guid EmployeeId { get; set; }

    public DateTime CheckInTime { get; set; }

    public DateTime CheckOutTime { get; set; }

    public DateTime Date { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }

    public virtual Employee Employee { get; set; }
}

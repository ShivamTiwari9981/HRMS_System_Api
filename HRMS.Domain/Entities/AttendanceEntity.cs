using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(AttendanceCode), IsUnique = true)]
    [Index(nameof(ClientId), nameof(EmployeeId), nameof(Date), IsUnique = true)]
    public class AttendanceEntity : BaseEntity
    {
        [Key]
        public Guid AttendanceId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual ClientEntity Client { get; set; }

        [Required]
        [MaxLength(20)]
        public string AttendanceCode { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        public DateTime InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public decimal? WorkingHours { get; set; }

        [Required]
        public DateTime Date { get; set; } // keep only date part
    }
}

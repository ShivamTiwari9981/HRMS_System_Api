using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRMS.Domain.Entities
{
    [Table("Attendance")]
    public class AttendanceEntity : BaseEntity
    {
        [Required]
        [Column("AttendanceCode")]
        [StringLength(20)]
        public string AttendanceCode { get; set; }
        [Required]
        [Column("EmployeeId")]
        public Guid EmployeeId { get; set; }
        [Required]
        [Column("CheckInTime")]
        public DateTime CheckInTime { get; set; }
        [Required]
        [Column("CheckOutTime")]
        public DateTime CheckOutTime { get; set; }
        [Required]
        [Column("Date")]
        public DateTime Date { get; set; }
    }
}

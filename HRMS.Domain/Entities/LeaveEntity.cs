using HRMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("Leave")]
    public class LeaveEntity : BaseEntity
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(500),MaxLength(500), MinLength(10)]
        public string Reason { get; set; }
        [Required]
        public LeaveStatus LeaveStatus { get; set; } 
    }
}

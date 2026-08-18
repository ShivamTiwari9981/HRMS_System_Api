using HRMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(EmployeeId), nameof(StartDate))]
    public class LeaveEntity : BaseEntity
    {
        [Key]
        public Guid LeaveId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public ClientEntity Client { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Reason { get; set; }

        public decimal TotalDays { get; set; }

        [Required]
        public LeaveStatus LeaveStatus { get; set; }  // Pending, Approved, Rejected


    }
    
}

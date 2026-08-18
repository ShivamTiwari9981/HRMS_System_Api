using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    public  class LeaveBalanceEntity : BaseEntity
    {
        [Key]
        public Guid LeaveBalanceId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public ClientEntity Client { get; set; }

        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public decimal TotalLeave { get; set; }
        public decimal UsedLeave { get; set; }
        public decimal RemainingLeave { get; set; }
    }
}

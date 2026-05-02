
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("Payroll")]
    public class PayrollEntity : BaseEntity
    {
        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        [MinLength(1)]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public decimal BasicSalary { get; set; }
        [Required]
        public decimal Bonus { get; set; }
        [Required]
        public decimal Deductions { get; set; }
        [Required]
        public decimal NetSalary { get; set; }
    }
}

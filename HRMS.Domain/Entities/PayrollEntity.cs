
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(EmployeeId), nameof(Month), nameof(Year), IsUnique = true)]
    public class PayrollEntity: BaseEntity
    {
        [Key]
        public Guid PayrollId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        public Guid EmployeeId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
    }
    
}

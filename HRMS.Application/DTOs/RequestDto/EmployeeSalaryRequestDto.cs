using System.ComponentModel.DataAnnotations.Schema;


namespace HRMS.Application.DTOs.RequestDto
{
    public class EmployeeSalaryRequestDto
    {

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal HRA { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Allowance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Deduction { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSalary { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public bool IsCurrent { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRMS.Domain.Entities
{
    [Table("Department")]
    public class DepartmentEntity : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string DepartmentCode { get; set; }

        [Required]
        [StringLength(200)]
        public string DepartmentName { get; set; }
       
    }
}

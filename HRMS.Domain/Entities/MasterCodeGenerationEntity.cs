using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("MasterCodeGeneration")]
    public class MasterCodeGenerationEntity : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string TableName { get; set; }

        [Required]
        [MinLength(1),MaxLength(3)]
        public string Prefix { get; set; }

        [Required]
        [MinLength(1)]
        public int LastNumber { get; set; }
    }
}

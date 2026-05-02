using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Table("Menu")]
    public class MenuEntity : BaseEntity
    {

        public int? ParentMenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        public string? MenuIcon { get; set; }

        [Required]
        public string RouterLink { get; set; }

        [Required]
        public int? DisplayOrder { get; set; }

        public bool? IsVisible { get; set; }
    }
}

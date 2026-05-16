using HRMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ParentMenuId), nameof(MenuName), IsUnique = true)]
    public class MenuEntity : BaseEntity
    {
        [Key]
        public Guid MenuId { get; set; } = Guid.NewGuid();
        public Guid? ParentMenuId { get; set; }

        [ForeignKey(nameof(ParentMenuId))]
        public MenuEntity ParentMenu { get; set; }

        public ICollection<MenuEntity> Children { get; set; }
           = new List<MenuEntity>();

        [Required]
        [MaxLength(200)]
        public string MenuName { get; set; }

        public string MenuIcon { get; set; }

        [MaxLength(300)]
        public string? RouterLink { get; set; }
        public bool IsVisible { get; set; } = true;

        public int? DisplayOrder { get; set; }

        public ICollection<PermissionEntity> Permissions { get; set; }
           = new List<PermissionEntity>();

        public MenuType MenuType { get; set; }
        
    }
   
}

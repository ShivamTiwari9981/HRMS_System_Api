using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId), nameof(MenuName), IsUnique = true)]
    public class MenuEntity : BaseEntity
    {
        [Key]
        public int MenuId { get; set; }

        public Guid ClientId { get; set; }

        public int? ParentMenuId { get; set; }

        [ForeignKey(nameof(ParentMenuId))]
        public MenuEntity ParentMenu { get; set; }

        public ICollection<MenuEntity> Children { get; set; }

        [Required]
        [MaxLength(200)]
        public string MenuName { get; set; }

        public string MenuIcon { get; set; }

        public string RouterLink { get; set; }

        public int? DisplayOrder { get; set; }
    }
   
}

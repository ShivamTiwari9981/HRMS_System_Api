namespace HRMS.Application.DTOs
{
    public class MenuDto : BaseDto
    {
        public int? ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string? MenuIcon { get; set; }
        public string RouterLink { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsVisible { get; set; }
    }
}

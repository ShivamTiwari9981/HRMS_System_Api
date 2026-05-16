namespace HRMS.Application.DTOs.ResponseDto
{
    public class MenuResponseDto
    {
        public Guid ParrentMenuId { get; set; }
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string RouterLink { get; set; }
        public bool IsVisible { get; set; }
        public int DisplayOrder { get; set; }
        public string MenuType { get; set; }
        public bool IsActive { get; set; }
    }
}

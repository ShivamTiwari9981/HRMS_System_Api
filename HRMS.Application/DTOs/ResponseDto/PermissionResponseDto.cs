using HRMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.ResponseDto
{
    public class PermissionResponseDto
    {
        public Guid PermissionId { get; set; }
        [Required]
        public Guid MenuId { get; set; }
        public PermissionAction Action { get; set; }
        [Required]
        public string PermissionKey { get; set; }
        public string? Description { get; set; }
    }
}

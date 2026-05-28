using HRMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Application.DTOs.RequestDto
{
    public class DesignationRequestDto
    {
        public Guid DesignationId { get; set; } 
        public string DesignationCode { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string DesignationName { get; set; }
        public string? Description { get; set; }

        public int? DisplayOrder { get; set; }

    }
}

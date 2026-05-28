using HRMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs
{
    public class DepartmentDto
    {
        public Guid DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }


        public DepartmentEntity GetEntity()
        {
            return new DepartmentEntity
            {
                DepartmentId = DepartmentId,
                DepartmentName = DepartmentName,
            };
        }
    }
}

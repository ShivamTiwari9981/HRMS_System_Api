
namespace HRMS.Application.DTOs
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public bool IsActive { get; set; }
    }
}

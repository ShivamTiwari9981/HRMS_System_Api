
namespace HRMS.Application.DTOs
{
    public class AttendanceDto
    {
        public string AttendanceCode { get; set; }
       
        public Guid EmployeeId { get; set; }
        
        public DateTime CheckInTime { get; set; }
      
        public DateTime CheckOutTime { get; set; }
       
        public DateTime Date { get; set; }
    }
}

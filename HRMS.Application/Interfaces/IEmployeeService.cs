using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;

namespace HRMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResponse<EmployeeResponseDto>> GetAllEmployees(EmployeeListRequestDto dto);
        ApiResponse<bool> AddEmployee(EmployeeRequestDto dto);
        ApiResponse<LoadCreateEmployeeMasterDto> GetDropdownList();
    }

}

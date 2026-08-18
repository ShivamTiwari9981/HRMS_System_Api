using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;
using System.Data;
using static HRMS.Application.Common.GenericProcedureCall;

namespace HRMS.Application.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private IMasterDataService _masterDataService;
        public EmployeeService(IUnitOfWork unitOfWork,
            ICurrentUserService currentSession, IMasterDataService masterDataService
            )
            : base(unitOfWork, currentSession)
        {
            _masterDataService = masterDataService;
        }
        public async Task<ApiResponse<bool>> IsEmployeeExist(Guid EmployeeId)
        {
            bool IsEmployeeExist = await _unitOfWork.EmployeeRepository.
                AnyAsync(x => x.ClientId == ClientId
                && x.EmployeeId == EmployeeId
                );

            return ApiResponse<bool>.Success(IsEmployeeExist);
        }

        
     
        
        public async Task<PagedResponse<EmployeeResponseDto>> GetAllEmployees(EmployeeListRequestDto dto)
        {
            try
            {

                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientId", ClientId),
                    new SqlParameter("@PageNumber", dto.PageNumber),
                    new SqlParameter("@PageSize", dto.PageSize),
                    new SqlParameter("@SearchText", (object?) dto.SearchText  ?? DBNull.Value),
                    new SqlParameter("@DepartmentId", (object?) dto.DepartmentId  ?? DBNull.Value),
                    new SqlParameter("@DesignationId", (object?) dto.DesignationId  ?? DBNull.Value),
                    new SqlParameter("@IsActive", (object?) dto.IsActive  ?? DBNull.Value),
                    new SqlParameter("@SortColumn",(object?) dto.SortColumn  ?? DBNull.Value),
                    new SqlParameter("@SortDirection",(object?) dto.SortDirection  ?? DBNull.Value),
                };
                var result = await ExecuteStoredProcedureDataSetAsync(
                    StoredProcedure.sp_GetEmployees,
                    param,
                    _unitOfWork.GetConnection()
                );
                var employees = CommonMethod.ConvertToList<EmployeeResponseDto>(result.Tables[0]);
                return new PagedResponse<EmployeeResponseDto>
                {
                    TotalRecords = employees.FirstOrDefault()?.TotalRecords ?? 0,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize,
                    Data = employees
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching employees: {ex.Message}", ex);
            }
        }

        public async Task<ApiResponse<EmployeeResponseDto>> GetEmployeeByIdAsync(Guid EmployeeId)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.FirstOrDefaultAsync(
                    x => x.ClientId == ClientId
                    && x.DepartmentId == EmployeeId
                    );

                var dto = EmployeeMapper.GetDto(employee,ClientId);

                return ApiResponse<DepartmentResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ApiResponse<bool> AddEmployee(EmployeeRequestDto dto)
        {
            try
            {
               
                var entity = EmployeeMapper.GetEntity(dto,ClientId);
                entity.Salary.EmployeeId = entity.EmployeeId;

                if(entity.IsLoginUser)
                    entity.User.EmployeeId = entity.EmployeeId;

                string jsonData = JsonConvert.SerializeObject(entity);

                var param = new List<SqlParameter>
                {
                    new SqlParameter("@JsonData", jsonData),
                    new SqlParameter("@CreatedBy", UserId),
                    new SqlParameter("@Err_No", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    },

                    new SqlParameter("@Err_Msg", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    }
                };
                

                var result = ExecuteStoredProcedure(
                    StoredProcedure.sp_AddEmployee,
                    param,
                    _unitOfWork.GetConnection()
                );

                int err_no = (int)(param.First(p => p.ParameterName == "@Err_No").Value ?? 0);
                string err_msg = param.First(p => p.ParameterName == "@Err_Msg").Value?.ToString() ?? string.Empty;

                if (err_no != 0)
                    return ApiResponse<bool>.Fail(err_no, err_msg);

                return ApiResponse<bool>.Success(true, err_msg);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(500, ex.Message);
            }
        }



        #region #EmployeeSalary
        public ApiResponse<bool> AddEmployeeSalary(EmployeeSalaryRequestDto dto)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(dto);

                var param = new List<SqlParameter>
                {
                    new SqlParameter("@JsonData", jsonData),
                    new SqlParameter("@CreatedBy", UserId),
                    new SqlParameter("@Err_No", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    },

                    new SqlParameter("@Err_Msg", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    }
                };


                var result = ExecuteStoredProcedureWithTransation(
                    StoredProcedure.Sp_EmployeeSalary,
                    param,
                    _unitOfWork.GetConnection(),
                    _unitOfWork.GetTransaction()
                );

                int err_no = (int)(param.First(p => p.ParameterName == "@Err_No").Value ?? 0);
                string err_msg = param.First(p => p.ParameterName == "@Err_Msg").Value?.ToString() ?? string.Empty;

                if (err_no != 0)
                    return ApiResponse<bool>.Fail(err_no, err_msg);

                return ApiResponse<bool>.Success(true, err_msg);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(500, ex.Message);
            }
        }
        #endregion

        public ApiResponse<LoadCreateEmployeeMasterDto> GetDropdownList()
        {
            try
            {

                var model = new LoadCreateEmployeeMasterDto();
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientId", ClientId),
                    
                };
                var result =  ExecuteStoredProcedure(
                    StoredProcedure.sp_LoadEmployeeDropdown,
                    param,
                    _unitOfWork.GetConnection()
                );

                model.Departments = CommonMethod.ConvertToList<EmployeeDepartmentDto>(result.Tables[0]);
                model.Designation = CommonMethod.ConvertToList<EmployeeDesignationDto>(result.Tables[1]);
                model.Gender = _masterDataService.GetGender();
                if(result.Tables.Count>2)
                {
                    model.Manager = CommonMethod.ConvertToList<EmployeeManagerDto>(result.Tables[2]);
                }
                return new ApiResponse<LoadCreateEmployeeMasterDto>
                {
                    ErrorNo=0,
                    Data= model
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching employees: {ex.Message}", ex);
            }
        }
    }
}

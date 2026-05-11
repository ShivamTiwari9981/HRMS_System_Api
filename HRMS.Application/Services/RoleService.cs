using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using static HRMS.Application.Common.GenericProcedureCall;


namespace HRMS.Application.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, ICurrentUserService currentSession) : base(unitOfWork, currentSession)
        {

        }

        public async Task<bool> IsRoleExist()
        {
            bool isRoleExist = await _unitOfWork.RoleRepository.AnyAsync(x => x.RoleId == RoleId && x.ClientId == ClientId);
            return isRoleExist;
        }

        public async Task<ApiResponse<List<RoleResponseDto>>> GetAllRole()
        {
            List<RoleEntity> roleEntity = await _unitOfWork.RoleRepository.WhereAsync(x => x.ClientId == ClientId);

            var dtoList = RoleMapper.ToDtoList(roleEntity);

            return ApiResponse<List<RoleResponseDto>>.Success(
                     dtoList, "Role created successfully!"
                );
        }

        public async Task<ApiResponse<string>> AddRole(RoleRequestDto dto)
        {
            try
            {
                var role = RoleMapper.ToEntity(dto, ClientId, UserId);

                await _unitOfWork.RoleRepository.AddAsync(role);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result)
                {
                    return ApiResponse<string>.Success(
                    role.RoleId.ToString(),
               "Role created successfully!"
                    );
                }
                return ApiResponse<string>.Fail(
                    1,
                    "Role could not be created!"
                );

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail(500, ex.Message);
            }
        }

        public ApiResponse<string> AssignPermissions(AssignRolePermissionRequestDto dto)
        {
            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientId", ClientId),
                    new SqlParameter("@RoleId", dto.RoleId),
                    new SqlParameter("@PermissionIds", dto.PermissionIds)
                    {
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "PermissionIdTableType"
                    },
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
                var result = ExecuteStoredProcedure(StoredProcedure.sp_AssignRolePermissions_TVP, param, _unitOfWork.GetConnection());
                int err_no = param.First(p => p.ParameterName == "@Err_No").Value != DBNull.Value
                ? Convert.ToInt32(param.First(p => p.ParameterName == "@Err_No").Value) : 0;
                string err_msg = param.First(p => p.ParameterName == "@Err_Msg").Value?.ToString() ?? string.Empty;

                if (err_no != 0)
                    return ApiResponse<string>.Fail(err_no, err_msg);

                return ApiResponse<string>.Success(null, err_msg);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail(500, ex.Message);
            }
        }
    }
}

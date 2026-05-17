using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using static HRMS.Application.Common.GenericProcedureCall;


namespace HRMS.Application.Services
{
    public class UserService :BaseService, IUserService
    {

        public UserService(IUnitOfWork unitOfWork, ICurrentUserService currentSession) : base(unitOfWork, currentSession)
        {
        }

        public ApiResponse<string> AssignRoles(AssignUsersRoleRequestDto dto)
        {
            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientId", ClientId),
                    new SqlParameter("@UserIds", dto.UserIds)
                    {
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "UserIdTableType"
                    },
                    new SqlParameter("@RoleIds", dto.RoleIds)
                    {
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "RoleIdTableType"
                    },
                    new SqlParameter("@UserId", UserId),
                    new SqlParameter("@Err_No", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    },
                    new SqlParameter("@Err_Msg", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    }
                };
                var result = ExecuteStoredProcedure(StoredProcedure.sp_AssignBulkUserRoles, param, _unitOfWork.GetConnection());
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

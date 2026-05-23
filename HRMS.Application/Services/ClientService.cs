using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using static HRMS.Application.Common.GenericProcedureCall;
namespace HRMS.Application.Services
{
    public class ClientService : BaseService, IClientService
    {
        public ClientService(IUnitOfWork unitOfWork, ICurrentUserService currentSession) : base(unitOfWork, currentSession) { }

        public async Task<bool> IsClientExit()
        {
            bool IsClientExist = await _unitOfWork.ClientRepository.AnyAsync(x => x.ClientId == ClientId);
            return IsClientExist;
        }
        public ApiResponse<LoginResponseDto> RegisterClient(ClientRequestDto dto)
        {
            try
            {
                LoginResponseDto res = new LoginResponseDto();
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@CompanyName", dto.CompanyName),
                    new SqlParameter("@CompanyEmail", dto.CompanyEmail),
                    new SqlParameter("@Phone", dto.Phone),
                    new SqlParameter("@SubscriptionStartDate", dto.SubscriptionStartDate),
                    new SqlParameter("@SubscriptionEndDate", dto.SubscriptionEndDate),
                    new SqlParameter("@GSTNumber", dto.GSTNumber),
                    new SqlParameter("@CompanyType", dto.ComapnyTypeId),
                    new SqlParameter("@SubscriptionPlanId", dto.SubscriptionPlanId),
                    new SqlParameter("@Address", dto.Address),
                    new SqlParameter("@Client_Id", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
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

                var result = ExecuteStoredProcedure(
                    StoredProcedure.Sp_Register_Client,
                    param,
                    _unitOfWork.GetConnection()
                );

                int err_no = (int)(param.First(p => p.ParameterName == "@Err_No").Value ?? 0);
                string err_msg = param.First(p => p.ParameterName == "@Err_Msg").Value?.ToString() ??string.Empty;
                if (err_no == 0)
                {
                    res.ClientId = Guid.Parse(param.First(p => p.ParameterName == "@Client_Id").Value.ToString());
                    res.UserId = UserId;
                }
                    

                if (err_no != 0)
                    return ApiResponse<LoginResponseDto>.Fail(err_no, err_msg);

                return ApiResponse<LoginResponseDto>.Success(res, err_msg);
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginResponseDto>.Fail(500, ex.Message);
            }
        }

    }
}

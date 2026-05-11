using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using HRMS.Shared.Helpers;
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
            bool IsClientExist = await _unitOfWork.ClientRepository.AnyAsync(x => x.ClientId == ClientId && x.ClientKey == ClientKey);
            return IsClientExist;
        }
        public ApiResponse<LoginResponseDto> RegisterClient(ClientRequestDto dto)
        {
            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientId", ClientId),
                    new SqlParameter("@ClientKey", dto.ClientKey),
                    new SqlParameter("@ClientName", dto.ClientName),
                    new SqlParameter("@CompanyName", dto.CompanyName),
                    new SqlParameter("@CompanyLogo", dto.CompanyLogo),
                    new SqlParameter("@Domain", dto.Domain),
                    new SqlParameter("@ContactPerson", dto.ContactPerson),
                    new SqlParameter("@CompanyEmail", dto.CompanyEmail),
                    new SqlParameter("@Phone", dto.Phone),
                    new SqlParameter("@ExpiryDate", dto.ExpiryDate),
                    new SqlParameter("@GSTNumber", dto.GSTNumber),
                    new SqlParameter("@Address", dto.Address),
                    new SqlParameter("@UpdatedBy", UserId),

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

                if (err_no != 0)
                    return ApiResponse<LoginResponseDto>.Fail(err_no, err_msg);

                return ApiResponse<LoginResponseDto>.Success(null, err_msg);
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginResponseDto>.Fail(500, ex.Message);
            }
        }

    }
}

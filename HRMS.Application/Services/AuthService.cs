using AutoMapper;
using HRMS.Application.Common;
using HRMS.Application.DTOs;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using HRMS.Shared.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HRMS.Application.Common.GenericProcedureCall;
using static HRMS.Shared.Constants.Global;

namespace HRMS.Application.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        public ApiResponse<string> UserSignUp(SignupRequestDto dto)
        {
            int err_no = 0;
            string err_msg = string.Empty;

            try
            {
                var pwdResult = PasswordHelper.HashPassword(dto.Password);

                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientKey", dto.ClientKey),
                    new SqlParameter("@UserName", dto.UserName),
                    new SqlParameter("@UserEmail", dto.UserEmail),
                    new SqlParameter("@HashPassword", pwdResult.hash),
                    new SqlParameter("@UserSalt", pwdResult.salt),
                    new SqlParameter("@CreatedBy", SystemUser.DefaultSystemUser),
                    new SqlParameter("@ErrNumber", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    },
                    new SqlParameter("@ErrMsg", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    }
                };
                   var result = ExecuteStoredProcedure(StoredProcedure.Sp_Sign_Up, param, _unitOfWork.GetConnection());
                        err_no = param.First(p => p.ParameterName == "@ErrNumber").Value != DBNull.Value
                        ? Convert.ToInt32(param.First(p => p.ParameterName == "@ErrNumber").Value): 0;
                        err_msg = param.First(p => p.ParameterName == "@ErrMsg").Value?.ToString() ?? "";
                
                if (err_no != 0)
                    return ApiResponse<string>.Fail(err_no, err_msg);

                return ApiResponse<string>.Success(null, "Signup successful");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail(500, ex.Message);
            }
        }


        //public ApiResponse<string> UserSignUp(SignupRequestDto dto)
        //{
        //    int err_no = 0;
        //    string err_msg = "";
        //    try
        //    {
        //        var pwd_result = PasswordHelper.HashPassword(dto.Password);
        //        var param = new List<SqlParameter>();
        //        param.Add(new SqlParameter("@ClientKey", dto.ClientKey));
        //        param.Add(new SqlParameter("@UserName", dto.UserName));
        //        param.Add(new SqlParameter("@UserEmail", dto.UserEmail));
        //        param.Add(new SqlParameter("@HashPassword", pwd_result.hash));
        //        param.Add(new SqlParameter("@UserSalt", pwd_result.salt));
        //        param.Add(new SqlParameter("@CreatedBy", SystemUser.DefaultSystemUser));
        //        param.Add(new SqlParameter("@ErrNumber", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_no));
        //        param.Add(new SqlParameter("@ErrMsg", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_msg));
        //        var result = GenericProcedureCall.ExecuteStoredProcedure(GenericProcedureCall.StoredProcedure.Sp_Sign_Up, param, _unitOfWork.GetConnection());
        //        err_no = (int)param.Find(x => x.ParameterName == "@ErrNumber")?.Value;
        //        err_msg = param.Find(x => x.ParameterName == "@ErrMsg")?.Value.ToString() ?? "";
        //        if (err_no != 0)
        //        {
        //            return ApiResponse<string>.Fail(err_no, err_msg);
        //        }
        //        return ApiResponse<string>.Success(null, "SignUp successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponse<string>.Fail(1, ex.Message);
        //    }
        //}
        public ApiResponse<LoginResponseDto> Login(LoginRequestDto dto)
        {
            int err_no = 0;
            string err_msg = string.Empty;

            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ClientKey", dto.ClientKey),
                    new SqlParameter("@UserEmail", dto.UserEmail),

                new SqlParameter("@ErrNumber", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                },

                    new SqlParameter("@ErrMsg", SqlDbType.VarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                var result = ExecuteStoredProcedure(
                    StoredProcedure.Sp_User_Login,
                    param,
                    _unitOfWork.GetConnection()
                );

                err_no = (int)(param.First(p => p.ParameterName == "@ErrNumber").Value ?? 0);
                err_msg = param.First(p => p.ParameterName == "@ErrMsg").Value?.ToString() ?? "";

                if (err_no != 0)
                    return ApiResponse<LoginResponseDto>.Fail(err_no, err_msg);

                var userDto = CommonMethod
                    .ConvertToList<UserDto>(result.Tables[0])
                    .FirstOrDefault();

                if (userDto == null)
                    return ApiResponse<LoginResponseDto>.Fail(1, "Invalid email or password");

                // Verify password
                if (!PasswordHelper.VerifyPassword(dto.Password, userDto.PasswordHash, userDto.UserSalt))
                    return ApiResponse<LoginResponseDto>.Fail(1, "Invalid email or password");

                // Generate token
                string token = GenerateToken(userDto);

                var responseDto = new LoginResponseDto(userDto, token);

                return ApiResponse<LoginResponseDto>.Success(responseDto, "Login successful");
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginResponseDto>.Fail(500, ex.Message);
            }
        }
        //public ApiResponse<LoginResponseDto> Login(LoginRequestDto dto)
        //{
        //    int err_no = 0;
        //    string err_msg = string.Empty;
        //    try
        //    {

        //        var pwd_result = PasswordHelper.HashPassword(dto.Password);
        //        var param = new List<SqlParameter>();
        //        new SqlParameter("@ClientKey", dto.ClientKey);
        //        new SqlParameter("@UserEmail", dto.UserEmail);
        //        new SqlParameter("@ErrNo", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_no);
        //        new SqlParameter("@ErrMsg", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, null, DataRowVersion.Current, err_msg);
        //        var result = ExecuteStoredProcedure(StoredProcedure.Sp_User_Login, param, _unitOfWork.GetConnection());
        //        err_no = (int)param.Find(x => x.ParameterName == "@ErrNo")?.Value;
        //        err_msg = param.Find(x => x.ParameterName == "@ErrMsg")?.Value.ToString() ?? "";
        //        if (err_no == 0)
        //        {
        //            var userDto = CommonMethod.ConvertToList<UserDto>(result.Tables[0]).FirstOrDefault();
        //            if (!PasswordHelper.VerifyPassword(dto.Password, userDto.PasswordHash, userDto.UserSalt))
        //                return ApiResponse<LoginResponseDto>.Fail(err_no, err_msg);

        //            string token = GenerateToken(userDto);
        //            var responseDto = new LoginResponseDto(userDto, token);
        //            return ApiResponse<LoginResponseDto>.Success(responseDto, err_msg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponse<LoginResponseDto>.Fail(1, ex.Message);
        //    }
        //}
        private string GenerateToken(UserDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto));

                // Get JWT key from configuration
                var jwtKey = _configuration["Jwt:Key"];
                var jwtIssuer = _configuration["Jwt:Issuer"];
                var jwtAudience = _configuration["Jwt:Audience"];

                if (string.IsNullOrWhiteSpace(jwtKey))
                    throw new InvalidOperationException("Jwt:Key is not configured in appsettings.json");

                // Create security key and credentials
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Build claims
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, dto.UserName),
                    new Claim(Claim_Types.RoleName, dto.RoleName),
                    new Claim(Claim_Types.ClientId, dto.ClientId.ToString()),
                    new Claim(Claim_Types.UserId,dto.UserId.ToString()),
                    new Claim(Claim_Types.IsCompanyProfileCreated,dto.IsCompanyProfileCreated.ToString()),
                };

                // Create and write token
                var token = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(50),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
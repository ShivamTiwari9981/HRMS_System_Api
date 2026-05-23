using AutoMapper;
using HRMS.Application.Common;
using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
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
        private readonly ISettingService _settingService;
        private readonly IEmailService _emailService;
        private readonly IOTPService _otpService;
        public AuthService(
            IUnitOfWork unitOfWork,
            ISettingService settingService, 
            IEmailService emailService, 
            IConfiguration configuration,
            IOTPService oTPService,
            ICurrentUserService currentSession) : base(unitOfWork, currentSession)
        {
            _settingService = settingService;
            _emailService = emailService;
            _otpService = oTPService;
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
                ? Convert.ToInt32(param.First(p => p.ParameterName == "@ErrNumber").Value) : 0;
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
        public ApiResponse<ClientRolePermissionDto> Login(LoginRequestDto dto)
        {
            int err_no = 0;
            string err_msg = string.Empty;
            ClientRolePermissionDto usrRoleResult = new ClientRolePermissionDto();

            try
            {
                var param = new List<SqlParameter>
                {
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
                    return ApiResponse<ClientRolePermissionDto>.Fail(err_no, err_msg);

                var userDto = CommonMethod
                    .ConvertToList<UserDto>(result.Tables[0])
                    .FirstOrDefault();

                if (userDto == null)
                    return ApiResponse<ClientRolePermissionDto>.Fail(1, "Invalid email or password");

                // Verify password
                if (!PasswordHelper.VerifyPassword(dto.Password, userDto.PasswordHash, userDto.UserSalt))
                    return ApiResponse<ClientRolePermissionDto>.Fail(1, err_msg);

                // Generate token
                if (userDto.IsCompanyProfileCreated)
                    usrRoleResult =  GetUserRolePermissionsAsync(userDto.ClientId,userDto.UserId);

                else
                {
                    usrRoleResult.user.UserId=userDto.UserId;
                    usrRoleResult.user.ClientId=userDto.ClientId;
                    usrRoleResult.user.UserName=userDto.UserName;
                    usrRoleResult.user.UserEmail = userDto.UserEmail;
                    usrRoleResult.user.IsCompanyProfileCreated=userDto.IsCompanyProfileCreated;
                    string token =  GenerateToken(usrRoleResult);
                    usrRoleResult.Token = token;
                }
               return ApiResponse<ClientRolePermissionDto>.Success(usrRoleResult, err_msg);
            }
            catch (Exception ex)
            {
                return ApiResponse<ClientRolePermissionDto>.Fail(500, ex.Message);
            }
        }
        public ClientRolePermissionDto GetUserRolePermissionsAsync(Guid clientId,Guid userId)
        {
           
            ClientRolePermissionDto clientRole = new();

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClientId", clientId),
                new SqlParameter("@UserId", userId),
            };

            var result =  ExecuteStoredProcedure(
                StoredProcedure.sp_GetUserRolePermissions,
                parameters,
                _unitOfWork.GetConnection());

            if (result.Tables.Count > 0)
            {
                clientRole.user = CommonMethod.ConvertToList<UserResponseDto>(result.Tables[0]).FirstOrDefault();
                clientRole.client = CommonMethod.ConvertToList<ClientResponseDto>(result.Tables[1]).FirstOrDefault();
                clientRole.role = CommonMethod.ConvertToList<RoleResponseDto>(result.Tables[2]);
                clientRole.menu = CommonMethod.ConvertToList<MenuResponseDto>(result.Tables[3]);
                clientRole.rolepermission = CommonMethod.ConvertToList<RolePermissionResponseDto>(result.Tables[4]);
            }

            string token = GenerateToken(clientRole);
            clientRole.Token = token;
            return clientRole;
        }

        public string GenerateToken(ClientRolePermissionDto result)
        {
            try
            {
               
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

                var claims = new List<Claim>
                {
                      new Claim(ClaimTypes.Name, result.user.UserName),
                      new Claim(Claim_Types.ClientId, result.user.ClientId.ToString()??string.Empty),
                      new Claim(Claim_Types.UserId, result.user.UserId.ToString()),
                      new Claim(Claim_Types.IsCompanyProfileCreated, result.user.IsCompanyProfileCreated.ToString())
                };

                foreach (var role in result.role.DistinctBy(x => x.RoleNames))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleNames));
                }

                // Add Permissions
                foreach (var permission in result.rolepermission.DistinctBy(x => x.PermissionKey))
                {
                    claims.Add(new Claim(Claim_Types.Permission, permission.PermissionKey));
                }
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

        public async Task<ApiResponse<bool>> SendOtpAsync(string userEmail)
        {
            try
            {
                bool isValidUser = await _unitOfWork
                    .UserRepository
                    .AnyAsync(x => x.UserEmail == userEmail);

                if (!isValidUser)
                {
                    return ApiResponse<bool>.Fail(1, "Invalid user");
                }

                bool isEmailOtpEnabled = await _settingService.IsEmailOtpEnabled();

                if (!isEmailOtpEnabled)
                {
                    return ApiResponse<bool>.Fail(1, "Email OTP setting is disabled!");
                }

                string otp = OtpHelper.GenerateOtp();

                var emailResponse = await _emailService.SendEmailOTP(userEmail, otp);

                if (!emailResponse.IsSuccess)
                {
                    return ApiResponse<bool>.Fail(1, emailResponse.Message);
                }

                await _otpService.SaveOTP(userEmail, otp);

                return ApiResponse<bool>.Success(true, "OTP sent successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(1, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> VerifyEmailOTP(string userEmail, string otp)
        {
            try
            {
              var result = await _otpService.VerifyOtp(userEmail, otp);
              return result;           
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(1, ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> ResetPassword(string userEmail, string password)
        {
            try
            {
                var dbResult = await _unitOfWork
                    .UserRepository
                    .FirstOrDefaultAsync(x => x.UserEmail == userEmail && x.IsActive ==true);

                if (dbResult == null)
                {
                    return ApiResponse<bool>.Fail(1, "Invalid user");
                }
                var pwdResult = PasswordHelper.HashPassword(password);

                dbResult.PasswordHash = pwdResult.hash;
                dbResult.UserSalt = pwdResult.salt;

                _unitOfWork.UserRepository.Update(dbResult);


                bool save = await _unitOfWork.SaveChangesAsync();

                if(!save)
                {
                    return ApiResponse<bool>.Fail(1, "Password update failed");
                }
                return ApiResponse<bool>.Success(true, "Password updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(1, ex.Message);
            }
        }
    }
}
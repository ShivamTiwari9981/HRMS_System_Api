using AutoMapper;
using HRMS.Application.Common;
using HRMS.Application.DTOs;
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
using HRMS.Shared.Constants;
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
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ICurrentUserService currentSession) : base(unitOfWork, currentSession)
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
        public async Task<ApiResponse<ClientRolePermissionDto>> Login(LoginRequestDto dto)
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
                    return ApiResponse<ClientRolePermissionDto>.Fail(1, "Invalid email or password");

                // Generate token
                if (userDto.IsCompanyProfileCreated)
                    usrRoleResult =  GetUserRolePermissionsAsync(userDto.ClientId,userDto.UserId);

                else
                {
                    usrRoleResult.clientUserResponse.UserId=userDto.UserId;
                    usrRoleResult.clientUserResponse.ClientId=userDto.ClientId;
                    usrRoleResult.clientUserResponse.UserName=userDto.UserName;
                    usrRoleResult.clientUserResponse.IsCompanyProfileCreated=userDto.IsCompanyProfileCreated;
                    string token =  GenerateToken(usrRoleResult);
                    usrRoleResult.Token = token;
                }
               return ApiResponse<ClientRolePermissionDto>.Success(usrRoleResult, "Login successful");
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
                clientRole.clientUserResponse = CommonMethod.ConvertToList<ClientUserResponseDto>(result.Tables[0]).FirstOrDefault();
                clientRole.RoleResponse = CommonMethod.ConvertToList<RoleResponseDto>(result.Tables[1]);
                clientRole.menuResponse = CommonMethod.ConvertToList<MenuResponseDto>(result.Tables[2]);
                clientRole.rolePermissionResponse = CommonMethod.ConvertToList<RolePermissionResponseDto>(result.Tables[3]);
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
                      new Claim(ClaimTypes.Name, result.clientUserResponse.UserName),
                      new Claim(Claim_Types.ClientId, result.clientUserResponse.ClientId.ToString()??string.Empty),
                      new Claim(Claim_Types.UserId, result.clientUserResponse.UserId.ToString()),
                      new Claim(Claim_Types.IsCompanyProfileCreated, result.clientUserResponse.IsCompanyProfileCreated.ToString())
                };

                foreach (var role in result.RoleResponse.DistinctBy(x => x.RoleNames))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleNames));
                }

                // Add Permissions
                foreach (var permission in result.rolePermissionResponse.DistinctBy(x => x.PermissionKey))
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
    }
}
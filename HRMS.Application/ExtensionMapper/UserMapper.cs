using HRMS.Application.DTOs.RequestDto;
using HRMS.Domain.Entities;
using HRMS.Shared.Helpers;


namespace HRMS.Application.ExtensionMapper
{
    public static class UserMapper
    {
        public static UserEntity GetEntity(
        UserRequestDto dto, Guid ClientId)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var pwdResult = PasswordHelper.HashPassword(dto.Password);

            return new UserEntity
            {
                UserId = Guid.NewGuid(),
                ClientId = ClientId,
                UserName = dto.UserName,
                PasswordHash = pwdResult.hash,
                UserSalt = pwdResult.salt,
                UserEmail = dto.UserEmail,
                Phone = dto.Phone,
                ProfileImagePath = dto.ProfileImagePath,
                FailedLoginAttempts = 0,
                LockoutEnd = null,
                IsLocked = false,
                IsCompanyProfileCreated = true,
            };
        }
    }
}

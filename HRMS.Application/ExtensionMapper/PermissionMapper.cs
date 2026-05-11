
using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static class PermissionMapper
    {
        public static PermissionEntity ToEntity(this PermissionRequestDto dto, Guid clientId, Guid userId)
        {
            return new PermissionEntity
            {
                PermissionName = dto.PermissionName,
                ClientId = clientId,
                CreatedBy = userId,
            };
        }

        public static List<PermissionResponseDto> ToDtoList(this List<PermissionEntity> entities)
        {
            return entities.Select(x => new PermissionResponseDto
            { 
                PermissionId = x.PermissionId,
                PermissionName = x.PermissionName,
            }).ToList();
        }

        public static PermissionResponseDto ToDto(this PermissionEntity entity)
        {
            return new PermissionResponseDto
            {
                PermissionId = entity.PermissionId,
                PermissionName = entity.PermissionName,
            };
        }

    }
}


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
                PermissionId = dto.PermissionId,
                MenuId = dto.MenuId,
                Action=dto.Action,
                PermissionKey = dto.PermissionKey,
                Description = dto.Description,
                ClientId = clientId,
                CreatedBy = userId,
            };
        }

        public static List<PermissionResponseDto> ToDtoList(this List<PermissionEntity> entities)
        {
            return entities.Select(x => new PermissionResponseDto
            { 

                PermissionId = x.PermissionId,
                MenuId = x.MenuId,
                Action = x.Action,
                PermissionKey = x.PermissionKey,
                Description = x.Description,
            }).ToList();
        }

        public static PermissionResponseDto ToDto(this PermissionEntity entity)
        {
            return new PermissionResponseDto
            {
                PermissionId = entity.PermissionId,
                MenuId = entity.MenuId,
                Action = entity.Action,
                PermissionKey = entity.PermissionKey,
                Description = entity.Description,
            };
        }

    }
}

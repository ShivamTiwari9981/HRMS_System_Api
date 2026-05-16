using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static  class RoleMapper
    {
        public static RoleEntity ToEntity(this RoleRequestDto dto,Guid clientId,Guid userId)
        {
            return new RoleEntity
            {
                RoleName = dto.RoleName,
                ClientId =  clientId,
                CreatedBy = userId,
            };
        }

        public static List<RoleResponseDto> ToDtoList(this List<RoleEntity> entities)
        {
            return entities.Select(x => new RoleResponseDto
            {
                RoleIds = x.RoleId,
                RoleNames = x.RoleName,
            }).ToList();
        }
    }
}

using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static class DesignationMapper
    {
        public static DesignationEntity GetEntity(
            this DesignationRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new DesignationEntity
            {
                DepartmentId = dto.DepartmentId,
                DesignationName = dto.DesignationName,
                DesignationCode = dto.DesignationCode,
                Description = dto.Description,
                DisplayOrder = dto.DisplayOrder,
            };
        }

        public static DesignationResponseDto GetDto(
            this DesignationEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new DesignationResponseDto
            {
                DesignationId = entity.DesignationId,
                DepartmentId = entity.DepartmentId,
                DesignationName = entity.DesignationName,
                DesignationCode = entity.DesignationCode,
                Description = entity.Description,
                DisplayOrder = entity.DisplayOrder,
            };
        }

        public static List<DesignationResponseDto> GetDtoList(
            this IEnumerable<DesignationEntity> entities)
        {
            return entities.Select(x => new DesignationResponseDto
            {
                DesignationId = x.DesignationId,
                DepartmentId = x.DepartmentId,
                DesignationName = x.DesignationName,
                DesignationCode = x.DesignationCode,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
            }).ToList();
        }
    }
}


using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static class DepartmentMapper
    {
        public static DepartmentEntity GetEntity(
            DepartmentRequestDto dto, Guid clientId)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new DepartmentEntity
            {
                ClientId = clientId,
                DepartmentId = dto.DepartmentId,
                DepartmentCode = dto.DepartmentCode,
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                DisplayOrder = dto.DisplayOrder,
            };
        }

        public static DepartmentResponseDto GetDto(
            this DepartmentEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new DepartmentResponseDto
            {
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.DepartmentName,
                DepartmentCode = entity.DepartmentCode,
                Description = entity.Description,
                DisplayOrder = entity.DisplayOrder,
                IsActive = entity.IsActive
            };
        }

        public static List<DepartmentResponseDto> GetDtoList(
            this IEnumerable<DepartmentEntity> entities)
        {
            return entities.Select(x => new DepartmentResponseDto
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName,
                DepartmentCode = x.DepartmentCode,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive,
            }).ToList();
        }
    }
}

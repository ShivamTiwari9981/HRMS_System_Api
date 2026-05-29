using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static class CityMapper
    {
        public static CityEntity GetEntity(
         CityRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new CityEntity
            {
                StateId = dto.StateId,
                CityId = dto.CityId,
                CityName = dto.CityName
            };
        }

        public static CityResponseDto GetDto(
            this CityEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new CityResponseDto
            {
                StateId = entity.StateId,
                CityId = entity.CityId,
                CityName = entity.CityName,
                IsActive = entity.IsActive
            };
        }

        public static List<CityResponseDto> GetDtoList(
            this IEnumerable<CityEntity> entities)
        {
            return entities.Select(x => new CityResponseDto
            {
                StateId = x.StateId,
                CityId = x.CityId,
                CityName = x.CityName,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}

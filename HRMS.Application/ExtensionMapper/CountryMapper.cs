using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;
namespace HRMS.Application.ExtensionMapper
{
    public static class CountryMapper
    {
        public static CountryEntity GetEntity(
            CountryRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new CountryEntity
            {
                CountryId = dto.CountryId,
                CountryName = dto.CountryName,
            };
        }

        public static CountryResponseDto GetDto(
            this CountryEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new CountryResponseDto
            {
                CountryId = entity.CountryId,
                CountryName = entity.CountryName,
            };
        }

        public static List<CountryResponseDto> GetDtoList(
            this IEnumerable<CountryEntity> entities)
        {
            return entities.Select(x => new CountryResponseDto
            {
                CountryId = x.CountryId,
                CountryName = x.CountryName,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}

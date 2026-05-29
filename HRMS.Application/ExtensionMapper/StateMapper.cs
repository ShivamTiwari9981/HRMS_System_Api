using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;

namespace HRMS.Application.ExtensionMapper
{
    public static class StateMapper
    {
        public static StateEntity GetEntity(
           StateRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new StateEntity
            {
                CountryId = dto.CountryId,
                StateName = dto.StateName,
            };
        }

        public static StateResponseDto GetDto(
            this StateEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new StateResponseDto
            {
                StateId = entity.StateId,
                StateName = entity.StateName,
                CountryId = entity.CountryId,
                IsActive = entity.IsActive
            };
        }

        public static List<StateResponseDto> GetDtoList(
            this IEnumerable<StateEntity> entities)
        {
            return entities.Select(x => new StateResponseDto
            {
                CountryId = x.CountryId,
                StateId = x.StateId,
                StateName = x.StateName,
                IsActive = x.IsActive,
            }).ToList();
        }
    }
}

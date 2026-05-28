using HRMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.RequestDto
{
    public class CityRequestDto
    {
        public Guid CityId { get; set; }

        [Required]
        public Guid StateId { get; set; }

        [Required]
        public string CityName { get; set; }

        public CityEntity GetEntity()
        {
            return new CityEntity
            {
                CityId = CityId,
                StateId = StateId,
                CityName = CityName,
            };
        }
    }
}

using HRMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.RequestDto
{
    public class CountryRequestDto
    {
        public Guid CountryId { get; set; }

        [Required]
        public string CountryName { get; set; }

        public CountryEntity GetEntity()
        {
            return new CountryEntity
            {
                CountryId = CountryId,
                CountryName = CountryName,
            };
        }
    }
}

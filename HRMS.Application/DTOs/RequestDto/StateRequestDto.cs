using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.RequestDto
{
    public class StateRequestDto
    {
        public Guid StateId { get; set; }
        [Required]
        public Guid CountryId { get; set; }
        [Required]
        public string StateName { get; set; }

        public StateEntity GetEntity()
        {
            return new StateEntity
            {
                CountryId = CountryId,
                StateId = StateId,
                StateName = StateName,
            };
        }
    }
}

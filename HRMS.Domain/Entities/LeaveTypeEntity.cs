using HRMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace HRMS.Domain.Entities
{

    [Index(nameof(ClientId), nameof(LeaveTypeCode), nameof(LeaveTypeName))]
    public class LeaveTypeEntity : BaseEntity
    {

        [Key]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public ClientEntity Client { get; set; }

        [Required]
        public string LeaveTypeCode { get; set; }

        [MaxLength(500)]
        public string LeaveTypeName { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Entities
{
    [Index(nameof(ClientId),IsUnique = true)]
    public class ErrorLogEntity : BaseEntity
    {
        [Key]
        public Guid ErrorLogId { get; set; } 

        public Guid? ClientId { get; set; }

        public string? ProcedureName { get; set; }
        public string? ErrorMessage { get; set; }
        public int? ErrorLine { get; set; } = 0;
        public int? ErrorNumber { get; set; } = 0;
        public int? ErrorState { get; set; } = 0;
        public int? ErrorSeverity { get; set; } = 0;
    }
}

using System;
using System.Collections.Generic;

namespace HRMS.Infrastructure.Models;

public partial class MasterCodeGeneration
{
    public Guid ClientId { get; set; }

    public Guid MasterCodeGenerationId { get; set; }

    public string TableName { get; set; }

    public string Prefix { get; set; }

    public int LastNumber { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsSynced { get; set; }

    public virtual Client Client { get; set; }
}

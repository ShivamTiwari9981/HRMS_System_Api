using System.ComponentModel.DataAnnotations;
using static HRMS.Shared.Constants.Global;


namespace HRMS.Infrastructure.Persistence.Seeders.Constants
{
    public static class MasterIdGenerationConstaints
    {
        public static readonly List<MasterIdGenerationModel> MasterIdGeneration = new()
        {
            new MasterIdGenerationModel
            {
                MasterCodeGenerationId = Guid.NewGuid(),
                TableName = MasterTable.Department,
                Prefix = CodePrefix.Department,
                LastNumber = 0,
            },
            new MasterIdGenerationModel
            {
                MasterCodeGenerationId = Guid.NewGuid(),
                TableName = MasterTable.Employee,
                Prefix = CodePrefix.Employee,
                LastNumber = 0,
            },
            new MasterIdGenerationModel
            {
                MasterCodeGenerationId = Guid.NewGuid(),
                TableName = MasterTable.User,
                Prefix = CodePrefix.User,
                LastNumber = 0,
            },
        };
    }
    public class MasterIdGenerationModel : CommonModel
    {
        [Key]
        public Guid MasterCodeGenerationId { get; set; }

        public Guid? ClientId { get; set; }
        [Required]

        [MaxLength(100)]
        public string TableName { get; set; }

        [Required]
        [MaxLength(3)]
        public string Prefix { get; set; }

        public int LastNumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;


namespace HRMS.Domain.Entities
{
    public class SubscriptionPlanEntity : BaseEntity
    {
        [Key]
        public Guid SubscriptionPlanId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string PlanName { get; set; }

        public decimal Price { get; set; }

        [Required]
        public int EmployeeLimit { get; set; } = 0;

        [Required]
        public int DurationInDays { get; set; } = 0;
    }
}

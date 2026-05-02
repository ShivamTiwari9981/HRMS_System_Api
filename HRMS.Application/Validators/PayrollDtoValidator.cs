using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class PayrollDtoValidator : AbstractValidator<PayrollDto>
    {
        public PayrollDtoValidator()
        {
            // EmployeeId
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid EmployeeId");

            // Month
            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12");

            // Year
            RuleFor(x => x.Year)
                .InclusiveBetween(2000, DateTime.UtcNow.Year)
                .WithMessage("Invalid year");

            // BasicSalary
            RuleFor(x => x.BasicSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Basic salary cannot be negative");

            // Bonus
            RuleFor(x => x.Bonus)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Bonus cannot be negative");

            // Deductions
            RuleFor(x => x.Deductions)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Deductions cannot be negative");

            // NetSalary
            RuleFor(x => x.NetSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Net salary cannot be negative");

            // 🔥 NetSalary calculation validation
            RuleFor(x => x)
                .Must(x => x.NetSalary == (x.BasicSalary + x.Bonus - x.Deductions))
                .WithMessage("Net salary must be equal to Basic + Bonus - Deductions");

            RuleFor(x => new { x.Month, x.Year })
            .Must(x =>
            {
                var payrollDate = new DateTime(x.Year, x.Month, 1);
                return payrollDate <= DateTime.UtcNow;
            })
            .WithMessage("Payroll cannot be created for future months");

        }
    }
}

using FluentValidation;
using HRMS.Application.DTOs;


namespace HRMS.Application.Validators
{
    public class LeaveDtoValidator : AbstractValidator<LeaveDto>
    {
        public LeaveDtoValidator()
        {
            // EmployeeId
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid EmployeeId");

            // StartDate
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required");

            // EndDate
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to start date");

            // Reason
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason is required")
                .MaximumLength(250).WithMessage("Reason must not exceed 250 characters");

            // LeaveStatus
            RuleFor(x => x.LeaveStatus)
                .IsInEnum().WithMessage("Invalid leave status");

            // Optional: StartDate should not be too old (business rule)
            RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddYears(-1))
                .WithMessage("Start date is too old");
        }
    }
}

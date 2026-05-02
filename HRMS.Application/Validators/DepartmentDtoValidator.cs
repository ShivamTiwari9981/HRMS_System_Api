using FluentValidation;
using HRMS.Application.DTOs;


namespace HRMS.Application.Validators
{

    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            // DepartmentCode
            RuleFor(x => x.DepartmentCode)
                .NotEmpty().WithMessage("Department code is required")
                .MaximumLength(20).WithMessage("Department code must not exceed 20 characters")
                .Matches(@"^[A-Za-z0-9_-]+$")
                .WithMessage("Department code can contain only letters, numbers, '_' or '-'");

            // DepartmentName
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("Department name is required")
                .MaximumLength(100).WithMessage("Department name must not exceed 100 characters")
                .Matches(@"^[A-Za-z\s]+$")
                .WithMessage("Department name should contain only letters and spaces");
        }
    }
}

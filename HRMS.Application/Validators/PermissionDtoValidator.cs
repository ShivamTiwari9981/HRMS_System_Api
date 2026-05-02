using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class PermissionDtoValidator : AbstractValidator<PermissionDto>
    {
        public PermissionDtoValidator()
        {
            // PermissionName
            RuleFor(x => x.PermissionName)
                .NotEmpty().WithMessage("Permission name is required")
                .MaximumLength(100).WithMessage("Permission name must not exceed 100 characters")
                .Matches(@"^[A-Za-z0-9\s._-]+$")
                .WithMessage("Permission name can contain letters, numbers, spaces, '.', '_' and '-'");
        }
    }
}

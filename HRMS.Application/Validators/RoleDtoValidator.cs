using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            // PermissionName
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters")
                .Matches(@"^[A-Za-z0-9\s._-]+$")
                .WithMessage("Permission name can contain letters, numbers, spaces, '.', '_' and '-'");
        }
    }
}

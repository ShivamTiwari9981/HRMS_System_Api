using FluentValidation;
using HRMS.Application.DTOs;
namespace HRMS.Application.Validators.BusinessValidator
{
    public class SignupInputValidator : AbstractValidator<SignupRequestDto>
    {
        public SignupInputValidator()
        {
            //RuleFor(x => x.CompanyName)
            //    .NotEmpty().WithMessage("Company name is required")
            //    .MaximumLength(150);

            //RuleFor(x => x.CompanyEmail)
            //    .NotEmpty().WithMessage("Company Email is required")
            //    .Matches(@"^[A-Za-z0-9\s._-]+$")
            //    .WithMessage("Company Email name can contain letters, numbers, spaces, '.', '_' and '-'");



            //// PermissionName
            //RuleFor(x => x.Email)
            //    .NotEmpty().WithMessage("Role name is required")
            //    .MaximumLength(100).WithMessage("Role name must not exceed 100 characters")
            //    .Matches(@"^[A-Za-z0-9\s._-]+$")
            //    .WithMessage("Permission name can contain letters, numbers, spaces, '.', '_' and '-'");


            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(4)
                .MaximumLength(50)
                .Matches(@"^[A-Za-z0-9._]+$")
                .WithMessage("Username can contain letters, numbers, '.' and '_'");


            RuleFor(x => x.Password)
           .NotEmpty()
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number");
        }
    }
}

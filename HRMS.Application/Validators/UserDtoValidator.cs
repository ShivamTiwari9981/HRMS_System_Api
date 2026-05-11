using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            // UserCode
            RuleFor(x => x.UserCode)
                .NotEmpty().WithMessage("User code is required")
                .MaximumLength(20)
                .Matches(@"^[A-Za-z0-9_-]+$")
                .WithMessage("User code can contain only letters, numbers, '_' or '-'");

            // FullName
            //RuleFor(x => x.FullName)
            //    .NotEmpty().WithMessage("Full name is required")
            //    .MaximumLength(100)
            //    .Matches(@"^[A-Za-z\s]+$")
            //    .WithMessage("Full name should contain only letters");

            // UserName
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(4)
                .MaximumLength(50)
                .Matches(@"^[A-Za-z0-9._]+$")
                .WithMessage("Username can contain letters, numbers, '.' and '_'");


            // Email
            //RuleFor(x => x.Email)
            //    .NotEmpty().WithMessage("Email is required")
            //    .EmailAddress().WithMessage("Invalid email format");

            // Phone
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Invalid phone number");

            // RoleId
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid RoleId");

            // ProfileImagePath (optional)
            RuleFor(x => x.ProfileImagePath)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.ProfileImagePath));

            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("Must contain uppercase")
            .Matches(@"[0-9]").WithMessage("Must contain number");
        }
    }
}

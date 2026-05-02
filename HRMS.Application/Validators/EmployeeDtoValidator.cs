using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            // EmployeeCode
            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Employee code is required")
                .MaximumLength(20)
                .Matches(@"^[A-Za-z0-9_-]+$")
                .WithMessage("Employee code can contain only letters, numbers, '_' or '-'");

            // FirstName
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50)
                .Matches(@"^[A-Za-z\s]+$")
                .WithMessage("First name should contain only letters");

            // LastName
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50)
                .Matches(@"^[A-Za-z\s]+$")
                .WithMessage("Last name should contain only letters");

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // Phone (Indian format)
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Invalid phone number");

            // DepartmentId
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid DepartmentId");

            // Designation
            RuleFor(x => x.Designation)
                .NotEmpty().WithMessage("Designation is required")
                .MaximumLength(100);

            // ProfileImagePath (optional)
            RuleFor(x => x.ProfileImagePath)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.ProfileImagePath));

            // DateOfJoining (optional but not future)
            RuleFor(x => x.DateOfJoining)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.DateOfJoining.HasValue)
                .WithMessage("Date of joining cannot be in the future");

            // Salary
            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0");

            // UserId
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid UserId");


        }
    }
}
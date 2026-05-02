using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class ClientDtoValidator : AbstractValidator<ClientDto>
    {
        public ClientDtoValidator()
        {
            // ClientCode
            RuleFor(x => x.ClientCode)
                .NotEmpty().WithMessage("Client code is required")
                .MaximumLength(20);

            // CompanyName
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(150);

            // CompanyLogo (optional but validate if present)
            RuleFor(x => x.CompanyLogo)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.CompanyLogo));

            // ClientName
            RuleFor(x => x.ClientName)
                .NotEmpty().WithMessage("Client name is required")
                .MaximumLength(100);

            // Domain
            RuleFor(x => x.Domain)
                .NotEmpty().WithMessage("Domain is required")
                .Matches(@"^(?!\-)(?:[a-zA-Z0-9\-]{1,63}\.)+[a-zA-Z]{2,}$")
                .WithMessage("Invalid domain format (e.g. example.com)");

            // ContactPerson (optional)
            RuleFor(x => x.ContactPerson)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.ContactPerson));

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // Phone
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Invalid phone number");

            // ExpiryDate
            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.UtcNow.Date)
                .WithMessage("Expiry date must be in the future");

            // Address (optional)
            RuleFor(x => x.Address)
                .MaximumLength(250)
                .When(x => !string.IsNullOrWhiteSpace(x.Address));
        }
    }
}

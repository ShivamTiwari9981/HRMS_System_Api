using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class MasterCodeGenerationDtoValidator : AbstractValidator<MasterCodeGenerationDto>
    {
        public MasterCodeGenerationDtoValidator()
        {
            // TableName
            RuleFor(x => x.TableName)
                .NotEmpty().WithMessage("Table name is required")
                .MaximumLength(100)
                .Matches(@"^[A-Za-z0-9_]+$")
                .WithMessage("Table name can contain only letters, numbers, and '_'");

            // Prefix
            RuleFor(x => x.Prefix)
                .NotEmpty().WithMessage("Prefix is required")
                .MaximumLength(10)
                .Matches(@"^[A-Z]+$")
                .WithMessage("Prefix must contain only uppercase letters");

            // LastNumber
            RuleFor(x => x.LastNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Last number must be 0 or greater");

            RuleFor(x => x.Prefix)
            .Length(2, 5)
            .WithMessage("Prefix must be between 2 and 5 characters");


            RuleFor(x => x.LastNumber)
            .LessThanOrEqualTo(999999)
            .WithMessage("Last number too large");
        }
    }
}

using FluentValidation;
using HRMS.Application.DTOs;

namespace HRMS.Application.Validators
{
    public class MenuDtoValidator : AbstractValidator<MenuDto>
    {
        public MenuDtoValidator()
        {
            // ParentMenuId (optional)
            RuleFor(x => x.ParentMenuId)
                .GreaterThan(0)
                .When(x => x.ParentMenuId.HasValue)
                .WithMessage("Invalid ParentMenuId");

            // MenuName
            RuleFor(x => x.MenuName)
                .NotEmpty().WithMessage("Menu name is required")
                .MaximumLength(100)
                .Matches(@"^[A-Za-z0-9\s]+$")
                .WithMessage("Menu name can contain only letters, numbers and spaces");

            // MenuIcon (optional)
            RuleFor(x => x.MenuIcon)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.MenuIcon));

            // RouterLink
            RuleFor(x => x.RouterLink)
                .NotEmpty().WithMessage("Router link is required")
                .MaximumLength(200)
                .Matches(@"^\/[A-Za-z0-9\-\/]*$")
                .WithMessage("Router link must start with '/' (e.g. /dashboard)");

            // DisplayOrder (optional but if present must be valid)
            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .When(x => x.DisplayOrder.HasValue)
                .WithMessage("Display order must be 0 or greater");

            // IsVisible (optional → default handled in backend)


            //RuleFor(x => x.ParentMenuId)
            //.NotEqual(x => x.Id)
            //.When(x => x.ParentMenuId.HasValue)
            //.WithMessage("Menu cannot be parent of itself");
        }
    }
}

using FluentValidation;
using HRMS.Application.DTOs;
using HRMS.Shared.Constants;

namespace HRMS.Application.Validators
{
    public class AttendanceValidator : AbstractValidator<AttendanceDto>
    {
        public AttendanceValidator()
        {
            // AttendanceCode
            RuleFor(x => x.AttendanceCode)
                .NotEmpty().WithMessage("Attendance code" + " " + DtoValidatorMessage.IsRequired)
                .MaximumLength(20).WithMessage("Attendance code" + " " + DtoValidatorMessage.MaxLength20);

            // EmployeeId
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId " + DtoValidatorMessage.IsRequired)
                .NotEqual(Guid.Empty).WithMessage(DtoValidatorMessage.InvalidEmployeeId);

            // CheckInTime
            RuleFor(x => x.CheckInTime)
                .NotEmpty().WithMessage("Check-in time " + DtoValidatorMessage.IsRequired);

            // CheckOutTime
            RuleFor(x => x.CheckOutTime)
                .NotEmpty().WithMessage("Check-out time " + DtoValidatorMessage.IsRequired)
                .GreaterThan(x => x.CheckInTime)
                .WithMessage(DtoValidatorMessage.CheckoutTimeMustbeGreaterThenCheckInTime);

            // Date
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date " + DtoValidatorMessage.IsRequired)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(DtoValidatorMessage.DateCanNotbeInTheFutureDate);

            RuleFor(x => x.CheckInTime.Date)
            .Equal(x => x.Date.Date)
            .WithMessage(DtoValidatorMessage.CheckInDateMustMatchAttendanceDate);


            RuleFor(x => x.CheckInTime)
            .Must(time => time.Hour >= 6 && time.Hour <= 12)
            .WithMessage("Check-in time must be between 6 AM and 12 PM");
        }

    }
}

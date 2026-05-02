using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRMS.Shared.Constants
{
    public static class DtoValidatorMessage
    {
        public static string IsRequired = " is required field.";
        public static string EmailValidation = "Please enter valid email address.";
        public static string MaxLength20 ="must not exceed 20 characters";
        public static string InvalidEmployeeId = "Invalid EmployeeId";
        public static string CheckoutTimeMustbeGreaterThenCheckInTime = "Check-out time must be greater than check-in time";
        public static string DateCanNotbeInTheFutureDate = "Date cannot be in the future";
        public static string CheckInDateMustMatchAttendanceDate = "Check-in date must match attendance date";


    }
}

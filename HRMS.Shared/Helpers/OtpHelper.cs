namespace HRMS.Shared.Helpers
{
    public static class OtpHelper
    {
        public static string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}

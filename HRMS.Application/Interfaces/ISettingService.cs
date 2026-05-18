namespace HRMS.Application.Interfaces
{
    public interface ISettingService
    {
        Task<bool> IsEmailOtpEnabled();
    }
}

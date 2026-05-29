using HRMS.Domain.Interfaces;
using HRMS.Shared.Enums;

namespace HRMS.Application.Interfaces
{
    public interface IUtilityService
    {
        (int err_no, string err_msg) GenerateMasterCode(string TableName);
        Task<int> GetNextDisplayOrderAsync(DisplayOrderType type, Guid Id);
    }
}

using HRMS.Domain.Interfaces;

namespace HRMS.Application.Interfaces
{
    public interface IUtilityService
    {
        (int err_no, string err_msg) GenerateMasterCode(string TableName);
    }
}

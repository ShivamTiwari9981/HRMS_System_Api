using AutoMapper;
using HRMS.Domain.Interfaces;

namespace HRMS.Application.Interfaces
{
    public interface IHelperFactory
    {
        IUnitOfWork UnitOfWork { get; }
        IUtilityService GetUtilityService();
    }
}

using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
namespace HRMS.Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        private ICurrentUserService _currentUserService;
        protected BaseService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        
        protected Guid ClientId => _currentUserService.ClientId;
        protected Guid UserId => _currentUserService.UserId;
        protected string ClientKey => _currentUserService.ClientKey;
        protected Guid RoleId => _currentUserService.RoleId;
    }
}

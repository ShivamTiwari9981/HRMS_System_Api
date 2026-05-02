using AutoMapper;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Interfaces;

namespace HRMS.Application.Helper
{
    public class HelperFactory(IUnitOfWork unitOfWork, ICurrentSession currentSession) : IHelperFactory
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentSession _currentSession = currentSession;

        public IUnitOfWork UnitOfWork => _unitOfWork;
        public ICurrentSession CurrentUser => _currentSession;

        private IUtilityService _utilityService;
        public IUtilityService GetUtilityService()
        {
            if (_utilityService == null)
            {
                _utilityService = new UtilityService(_unitOfWork, _currentSession);
            }
            return _utilityService;
        }
    }
}

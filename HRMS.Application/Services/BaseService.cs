using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
namespace HRMS.Application.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //protected TDestination MapToEntity<TDestination>(object source)
        //{
        //    return _mapper.Map<TDestination>(source);
        //}

        //// Entity → DTO
        //protected TDestination MapToDto<TDestination>(object source)
        //{
        //    return _mapper.Map<TDestination>(source);
        //}

        //// List mapping
        //protected List<TDestination> MapList<TDestination>(object source)
        //{
        //    return _mapper.Map<List<TDestination>>(source);
        //}
        //protected Guid ClientId => _currentsession.ClientId;
        //protected Guid UserId => _currentsession.UserId;
    }
}

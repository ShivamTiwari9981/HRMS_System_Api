using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using static HRMS.Shared.Constants.Global;

namespace HRMS.Application.Services
{
    public class DesignationService : BaseService, IDesignationService
    {
        private readonly IUtilityService _utilityService;
        public DesignationService(IUnitOfWork unitOfWork,
            ICurrentUserService currentSession, IUtilityService utilityService) : base(unitOfWork, currentSession)
        {
            _utilityService = utilityService;
        }

        public async Task<ApiResponse<bool>> IsDesignationExist(Guid DesignationId)
        {
            bool IsDesignationExist = await _unitOfWork.DesignationRepository.
                AnyAsync(x => x.ClientId == ClientId
                && x.DesignationId == DesignationId
                );

            return ApiResponse<bool>.Success(IsDesignationExist);
        }


        public async Task<ApiResponse<List<DesignationResponseDto>>> GetAllDesignationsAsync()
        {
            try
            {
                var deaprtmentList = await _unitOfWork.DesignationRepository.WhereAsync(x => x.ClientId == ClientId);

                var dtoList = DesignationMapper.GetDtoList(deaprtmentList);


                return ApiResponse<List<DesignationResponseDto>>.Success(dtoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<DesignationResponseDto>> GetDesignationByIdAsync(Guid DesignationId)
        {
            try
            {
                var deaprtment = await _unitOfWork.DesignationRepository.FirstOrDefaultAsync(
                    x => x.ClientId == ClientId
                    && x.DesignationId == DesignationId
                    );

                var dto = DesignationMapper.GetDto(deaprtment);

                return ApiResponse<DesignationResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> AddDesignationAsync(DesignationRequestDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var codeResult = _utilityService.GenerateMasterCode(MasterTable.Designation);

                if (codeResult.err_no != 0)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<bool>.Fail(1, codeResult.err_msg);

                }

                var entity = DesignationMapper.GetEntity(dto);

                entity.DesignationCode = codeResult.err_msg;
                entity.ClientId = ClientId;
                entity.DisplayOrder = await GetNextDesignationDisplayOrderAsync(entity.DepartmentId);

                await _unitOfWork.DesignationRepository.AddAsync(entity);
                var result = await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ApiResponse<bool>> UpdateDesignationAsync(DesignationRequestDto dto)
        {
            try
            {
                var dbResult = await _unitOfWork.DesignationRepository.
                    FirstOrDefaultAsync(x => x.ClientId == ClientId &&
                     x.DesignationId == dto.DesignationId && x.IsActive == true);


                dbResult.DesignationName = dto.DesignationName;
                _unitOfWork.DesignationRepository.Update(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();

                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeactivateDesignationAsync(Guid DesignationId)
        {
            try
            {
                var dbResult = await _unitOfWork.DesignationRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DesignationId == DesignationId && x.IsActive == true);

                await _unitOfWork.DesignationRepository.SoftDeleteAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> ReopenDesignationAsync(Guid DesignationId)
        {
            try
            {
                var dbResult = await _unitOfWork.DesignationRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DesignationId == DesignationId && x.IsActive == false);

                await _unitOfWork.DesignationRepository.ReopenAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<int> GetNextDesignationDisplayOrderAsync(Guid departmentId)
        {
            int maxDisplayOrder = await _unitOfWork.DesignationRepository
            .MaxAsync(x=> x.ClientId == ClientId && x.DepartmentId == departmentId,
             x => x.DisplayOrder
             ) ?? 0;

            return maxDisplayOrder;
        }


    }
}

using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Interfaces;
using HRMS.Shared.Enums;
using static HRMS.Shared.Constants.Global;

namespace HRMS.Application.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IUtilityService _utilityService;
        public DepartmentService(IUnitOfWork unitOfWork,
            ICurrentUserService currentSession , IUtilityService utilityService) : base(unitOfWork, currentSession)
        {
            _utilityService = utilityService;
        }

        public async Task<ApiResponse<bool>>IsDepartmentExist(Guid DepartmentId)
        {
            bool IsDepartmentExist = await _unitOfWork.DepartmentRepository.
                AnyAsync(x => x.ClientId == ClientId
                && x.DepartmentId == DepartmentId
                );

            return ApiResponse<bool>.Success(IsDepartmentExist);
        }

        public async Task<ApiResponse<bool>> IsDepartmentExistByName(string DepartmentName)
        {
            bool IsDepartmentExist = await _unitOfWork.DepartmentRepository.
                AnyAsync(x => x.ClientId == ClientId
                && x.DepartmentName == DepartmentName
                );

            return ApiResponse<bool>.Success(IsDepartmentExist);
        }


        public async Task<ApiResponse<List<DepartmentResponseDto>>> GetAllDepartmentsAsync()
        {
            try
            {
                var deaprtmentList = await _unitOfWork.DepartmentRepository.WhereAsync(x => x.ClientId == ClientId);
                var dtoList = DepartmentMapper.GetDtoList(deaprtmentList);

                return ApiResponse<List<DepartmentResponseDto>>.Success(dtoList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<DepartmentResponseDto>> GetDepartmentByIdAsync(Guid DepartmentId)
        {
            try
            {
                var deaprtment = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(
                    x => x.ClientId == ClientId
                    && x.DepartmentId == DepartmentId
                    );

                var dto = DepartmentMapper.GetDto(deaprtment);

                return ApiResponse<DepartmentResponseDto>.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> AddDepartmentAsync(DepartmentRequestDto dto)
        {
            try
            {
                bool IsDepartmentExist = await _unitOfWork.DepartmentRepository.AnyAsync(x => x.ClientId == ClientId
                 && x.DepartmentName == dto.DepartmentName
                );

                if (IsDepartmentExist)
                    return ApiResponse<bool>.Fail(1, "Department already exist");

                await _unitOfWork.BeginTransactionAsync();
                var codeResult = _utilityService.GenerateMasterCode(MasterTable.Department);

                if(codeResult.err_no!=0)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<bool>.Fail(1,codeResult.err_msg);
                }

                var entity = DepartmentMapper.GetEntity(dto,ClientId);
                entity.DepartmentCode = codeResult.err_msg;
                entity.CreatedBy = UserId;
                entity.DisplayOrder = entity.DisplayOrder = await _utilityService.GetNextDisplayOrderAsync(DisplayOrderType.Department, entity.DepartmentId);

                await _unitOfWork.DepartmentRepository.AddAsync(entity);
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

        public async Task<ApiResponse<bool>> UpdateDepartmentAsync(DepartmentRequestDto dto)
        {
            try
            {
                var dbResult = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DepartmentName == dto.DepartmentName && x.IsActive == true);

                if(!string.IsNullOrWhiteSpace(dbResult.DepartmentName))
                    return ApiResponse<bool>.Fail(1,"Department already exists!");

                dbResult = DepartmentMapper.UpdateEntity(dto, dbResult);
                _unitOfWork.DepartmentRepository.Update(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeactivateDepartmentAsync(Guid departmentId)
        {
            try
            {
                var dbResult = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DepartmentId == departmentId && x.IsActive == true);

                dbResult.IsActive = false;
               await _unitOfWork.DepartmentRepository.SoftDeleteAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> ActivateDepartmentAsync(Guid departmentId)
        {
            try
            {
                var dbResult = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DepartmentId == departmentId && x.IsActive == false);

                dbResult.IsActive = true;
                await _unitOfWork.DepartmentRepository.ActivateAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

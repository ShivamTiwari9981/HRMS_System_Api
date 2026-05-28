using HRMS.Application.DTOs;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
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


        public async Task<ApiResponse<List<DepartmentEntity>>> GetAllDepartmentsAsync()
        {
            try
            {
                var deaprtmentList = await _unitOfWork.DepartmentRepository.WhereAsync(x => x.ClientId == ClientId);
                return ApiResponse<List<DepartmentEntity>>.Success(deaprtmentList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<DepartmentEntity>> GetDepartmentByIdAsync(Guid DepartmentId)
        {
            try
            {
                var deaprtmentList = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(
                    x => x.ClientId == ClientId
                    && x.DepartmentId == DepartmentId
                    );
                return ApiResponse<DepartmentEntity>.Success(deaprtmentList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> SaveAsync(DepartmentEntity department)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var codeResult = _utilityService.GenerateMasterCode(MasterTable.Department);

                if(codeResult.err_no!=0)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<bool>.Fail(1,codeResult.err_msg);
                    
                }
                department.DepartmentCode = codeResult.err_msg;
                department.ClientId = ClientId;

                await _unitOfWork.DepartmentRepository.AddAsync(department);
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

        public async Task<ApiResponse<bool>> UpdateAsync(DepartmentEntity department)
        {
            try
            {
                var dbResult = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DepartmentId == department.DepartmentId && x.IsActive == true);


                dbResult.DepartmentName = department.DepartmentName;
                _unitOfWork.DepartmentRepository.Update(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> DeactivateAsync(Guid departmentId)
        {
            try
            {
                var dbResult = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DepartmentId == departmentId && x.IsActive == true);

               await _unitOfWork.DepartmentRepository.SoftDeleteAsync(dbResult);

                var result = await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<bool>> RepopenAsync(Guid departmentId)
        {
            try
            {
                var dbResult = await _unitOfWork.DepartmentRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId
                && x.DepartmentId == departmentId && x.IsActive == false);

                await _unitOfWork.DepartmentRepository.ReopenAsync(dbResult);

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

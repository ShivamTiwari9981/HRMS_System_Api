using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Application.ExtensionMapper;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;

namespace HRMS.Application.Services
{
    public class PermissionService : BaseService, IPermissionService
    {
        public PermissionService(IUnitOfWork unitOfWork, ICurrentUserService currentSession) : base(unitOfWork, currentSession)
        {

        }

        public async Task<ApiResponse<PermissionResponseDto>> GetPermissionById(Guid permissionId)
        {
           var dbResult = await _unitOfWork.PerimssionRepository.FirstOrDefaultAsync(x => x.ClientId == ClientId && x.PermissionId == permissionId);

            var dto = PermissionMapper.ToDto(dbResult);
            return ApiResponse<PermissionResponseDto>.Success(
                    dto
               );
        }

        public async Task<ApiResponse<List<PermissionResponseDto>>> GetAllPermission()
        {
            List<PermissionEntity> permissionEntity = await _unitOfWork.PerimssionRepository.WhereAsync(x => x.ClientId == ClientId);

            var dtoList = PermissionMapper.ToDtoList(permissionEntity);

            return ApiResponse<List<PermissionResponseDto>>.Success(
                     dtoList
                );
        }

        public async Task<ApiResponse<string>> AddPermission(PermissionRequestDto dto)
        {
            try
            {
                var permissionEntity = PermissionMapper.ToEntity(dto, ClientId, UserId);

                await _unitOfWork.PerimssionRepository.AddAsync(permissionEntity);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result)
                {
                    return ApiResponse<string>.Success(
                    permissionEntity.PermissionId.ToString(),
               "Permission created successfully!"
                    );
                }
                return ApiResponse<string>.Fail(
                    1,
                    "Permission could not be created!"
                );

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail(500, ex.Message);
            }
        }
    }
}

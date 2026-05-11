using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;


namespace HRMS.Application.Services
{
    public class UserService :BaseService, IUserService
    {

        public UserService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }

        #region Role
        //public async Task<RoleEntity>GetRoleByRoleName(string roleName)
        //{
        //   return  await _unitOfWork.RoleRepository
        //    .FirstOrDefaultAsync(r => r.RoleName == roleName);
        //}
        #endregion
    }
}

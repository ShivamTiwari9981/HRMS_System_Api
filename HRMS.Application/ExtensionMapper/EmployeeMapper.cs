using HRMS.Application.DTOs.RequestDto;
using HRMS.Application.DTOs.ResponseDto;
using HRMS.Domain.Entities;
namespace HRMS.Application.ExtensionMapper
{
    public static class EmployeeMapper
    {
        public static EmployeeEntity GetEntity(
        EmployeeRequestDto dto, Guid clientId)
        {
            ArgumentNullException.ThrowIfNull(dto);


            

            return new EmployeeEntity
            {
                EmployeeId = Guid.NewGuid(),
                ClientId = clientId,
                EmployeeEmail = dto.EmployeeEmail,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId,
                JoiningDate = dto.JoiningDate,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                AddressLine1 = dto.AddressLine1,
                CountryId = dto.CountryId,
                StateId = dto.StateId,
                CityId = dto.CityId,
                PostalCode = dto.PostalCode,
                EmergencyContact = dto.EmergencyContact,
                ManagerId = dto.ManagerId,
                IsLoginUser = dto.IsLoginUser,
                User = UserMapper.GetEntity(dto.User,clientId),
               
                Salary = EmployeeSalaryMapper.GetEntity(dto.Salary)

            };

           
        }
        public static EmployeeResponseDto GetDto(
            this EmployeeEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new EmployeeResponseDto
            {
                DesignationId = entity.DesignationId,
                DepartmentId = entity.DepartmentId,
                EmployeeId = entity.EmployeeId,
                EmployeeCode = entity.EmployeeCode,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EmployeeEmail = entity.EmployeeEmail,
                Phone = entity.Phone,
                JoiningDate = entity.JoiningDate,
                BirthDate = entity.BirthDate,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                CountryId = entity.CountryId,
                StateId = entity.StateId,
                CityId = entity.CityId,
                PostalCode = entity.PostalCode,
                EmergencyContact = entity.EmergencyContact,
                ManagerId = entity.ManagerId,
                IsLoginUser = entity.IsLoginUser,
            };
        }
    }
}

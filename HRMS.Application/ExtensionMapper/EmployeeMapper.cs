using HRMS.Application.DTOs.RequestDto;
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
    }
}

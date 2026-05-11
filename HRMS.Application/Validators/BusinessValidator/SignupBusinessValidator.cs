using FluentValidation;
using HRMS.Application.DTOs;
using HRMS.Domain.Interfaces;
namespace HRMS.Application.Validators
{
    public class SignupBusinessValidator 
    {
        private readonly IUnitOfWork _uow;
        public SignupBusinessValidator(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task ValidateAsync(SignupRequestDto request)
        {
          
            //if (await _uow.ClientRepository
            //    .FindAnyAsync(x => x.CompanyEmail == request.CompanyEmail && x.IsActive))
            //    throw new Exception("Company already exists");

            //if (await _uow.UserRepository
            //    .FindAnyAsync(x => x.Email == request.Email && x.IsActive))
            //    throw new Exception("User already exists");
        }
    }
 }

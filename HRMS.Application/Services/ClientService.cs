using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
namespace HRMS.Application.Services
{
    public class ClientService : BaseService, IClientService
    {
        public ClientService(IUnitOfWork unitOfWork): base(unitOfWork) { }
        //public async Task<ClientEntity> GetClientByEmail(string email)
        //{
        //    var client = await _unitOfWork.ClientRepository.FirstOrDefaultAsync(x => x.CompanyEmail ==email && x.IsActive==true);
        //    if (string.IsNullOrEmpty(client.CompanyName))
        //        return default(ClientEntity);   
        //    return client;
        //}
    }
}

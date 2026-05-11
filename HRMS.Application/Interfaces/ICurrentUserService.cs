namespace HRMS.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        Guid ClientId { get; }
        string ClientKey { get; }
        Guid RoleId { get; }
    }
}

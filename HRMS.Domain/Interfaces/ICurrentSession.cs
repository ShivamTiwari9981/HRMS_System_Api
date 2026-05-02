namespace HRMS.Domain.Interfaces
{
    public interface ICurrentSession
    {
        string ClientId { get; }
        string UserId { get; }
        string UserName { get; }
        string RoleName { get; }
        bool IsAuthenticated { get; }
    }
}

namespace api.Domain.Interfaces
{
    public interface IUsersActionsLogService
    {
        void Log(string action, string userEmail);
    }
}

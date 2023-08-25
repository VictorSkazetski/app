using api.Data.Interfaces;

namespace api.Data;

public class UserActionsHistoryEntity : IEntityWithTypedId<int>
{
    public int Id { get; set; }

    public string Action { get; set; }

    public string UserEmail { get; set; }

    public DateTime DateTime { get; set; }
}

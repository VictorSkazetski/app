namespace api.Data.Interfaces
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; set; }
    }
}

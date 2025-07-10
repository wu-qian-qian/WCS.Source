namespace Common.Domain;

public abstract class IEntity:ISoftDelete
{
    public IEntity(Guid id)
    {
        Id = id;
        IsDeleted = false;
    }
    public Guid Id { get;init; }
    public bool IsDeleted { get; private set; }
    public void SoftDelete()
    {
        IsDeleted = true;
    }
}
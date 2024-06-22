namespace TaskManagement.Domain.Entities.Base;

public class Entity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreateDate { get; protected set; }
    public DateTimeOffset? LastUpdateDate { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTimeOffset? DeleteDate { get; protected set; }

    protected Entity() {}

    protected Entity(Guid id, DateTimeOffset createDate) 
    {
        if (id == default) throw new Exception($"Object Id is invalid - {GetType().Name}");

        Id = id;
        CreateDate = createDate;
    }

    protected void Update(DateTimeOffset updateDate) 
    {
        if (IsDeleted) throw new Exception($"Object is already deleted - {GetType().Name}");

        LastUpdateDate = updateDate;
    }

    protected void Delete(DateTimeOffset deleteDate) 
    {
        if (IsDeleted) throw new Exception($"Object is already deleted - {GetType().Name}");

        DeleteDate = deleteDate;
        IsDeleted = true;
    }
}

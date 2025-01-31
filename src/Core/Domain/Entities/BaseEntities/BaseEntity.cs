namespace Domain.Entities.BaseEntities;
public interface IEntity;
public abstract class BaseEntity<TKey> : IEntity
{
    public TKey Id { get; set; } = default!;

    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ModifiedDate { get; set; }

    public bool IsRemoved { get; set; } = false;
}

public abstract class BaseEntity : BaseEntity<int>;
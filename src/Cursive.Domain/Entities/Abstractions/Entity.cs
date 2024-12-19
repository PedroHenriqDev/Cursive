using Cursive.Domain.Validations;

namespace Cursive.Domain.Entities.Abstractions;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public Entity(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public Entity()
    {
    }

    public abstract Validation Validate();
}

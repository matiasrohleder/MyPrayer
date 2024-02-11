namespace Entities.Models;

public abstract class Entity
{
    public Entity()
    {
        Name = nameof(Entity);
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}
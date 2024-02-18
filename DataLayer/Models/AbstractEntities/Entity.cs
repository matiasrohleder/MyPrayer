using DataLayer.Interfaces;

namespace DataLayer.Models.AbstractEntities;

public abstract class Entity : IEntity
{
    public Entity()
    {
        Name = nameof(Entity);
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }
    public Guid LastEditorId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastEditedDate { get; set; }
}
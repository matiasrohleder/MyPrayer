using DataLayer.Models.AbstractEntities;

namespace Entities.Models;

public class Category : Entity
{
    public Category()
    {
        Name = nameof(Category);
        Active = true;
    }

    public bool Active { get; set; }

    public int Order { get; set; }
}
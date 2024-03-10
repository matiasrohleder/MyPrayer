using Entities.Models;

namespace WebApp.Models;

public class CategoryViewModel
{
    public bool Active { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }

    public CategoryViewModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Active = true;
    }

    public CategoryViewModel(Category category)
    {
        Active = category.Active;
        Id = category.Id;
        Name = category.Name;
        Order = category.Order;
    }

    public Category ToEntity()
    {
        Category category = new()
        {
            Active = Active,
            Id = Id,
            Name = Name,
            Order = Order
        };

        return category;
    }
}
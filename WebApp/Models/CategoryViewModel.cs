using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class CategoryViewModel
{
    public bool Active { get; set; }

    public Guid Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categor\xeda es requerido")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El orden de la categor\xeda es requerido")]
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
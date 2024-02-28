using Entities.Models;

namespace WebAPI.DTOs
{
    public class CategoryItem
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public CategoryItem(Category category)
        {
            Name = category.Name;
            Order = category.Order;
        }
    }
}
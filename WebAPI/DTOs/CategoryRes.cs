using Entities.Models;

namespace WebAPI.DTOs
{
    public class CategoryRes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        
        public CategoryRes(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Order = category.Order;
        }
    }
}
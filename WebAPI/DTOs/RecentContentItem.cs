using Entities.Models;

namespace WebAPI.DTOs
{
    public class RecentContentItem
    {
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public int Order { get; set; }
        public List<ContentDTO> Contents { get; set; }

        public RecentContentItem(Category category)
        {
            Category = category.Name;
            CategoryId = category.Id;
            Order = category.Order;
            Contents = new List<ContentDTO>();
        }
    }
}
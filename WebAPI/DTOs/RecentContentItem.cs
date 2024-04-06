using Entities.Models;

namespace WebAPI.DTOs
{
    public class RecentContentItem
    {
        public CategoryRes Category { get; set; }
        public List<ContentRes> Contents { get; set; }

        public RecentContentItem(Category category)
        {
            Category = new CategoryRes(category);
            Contents = new List<ContentRes>();
        }
    }
}
using Entities.Models;

namespace WebAPI.DTOs
{
    public class RecentContentItem
    {
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public int Order { get; set; }
        public List<RecentContentDTO> Contents { get; set; }

        public RecentContentItem(Category category)
        {
            Category = category.Name;
            CategoryId = category.Id;
            Order = category.Order;
            Contents = new List<RecentContentDTO>();
        }
    }

    public class RecentContentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ShowDate { get; set; }
        public string Link { get; set; }
        
        // TO-DO: add image

        public RecentContentDTO(Content content)
        {
            Name = content.Name;
            Description = content.Description;
            ShowDate = content.ShowDate;
            Link = content.Link;
        }
    }
}
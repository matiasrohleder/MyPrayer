using Entities.Models;

namespace WebAPI.DTOs
{
    public class ContentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ShowDate { get; set; }
        public string Link { get; set; }
        
        // TO-DO: add image

        public ContentDTO(Content content)
        {
            Name = content.Name;
            Description = content.Description;
            ShowDate = content.ShowDate;
            Link = content.Link;
        }
    }
}
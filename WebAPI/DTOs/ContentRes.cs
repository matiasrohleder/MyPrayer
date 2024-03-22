using Entities.Models;

namespace WebAPI.DTOs
{
    public class ContentRes
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ShowDate { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        
        public ContentRes(Content content)
        {
            Name = content.Name;
            Description = content.Description;
            ShowDate = content.ShowDate;
            Link = content.Link;
            Image = "https://lastfm.freetls.fastly.net/i/u/ar0/251f6b43bb78fcc9d01e7cd8c20337a0.jpg";
        }
    }
}
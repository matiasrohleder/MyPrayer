using Entities.Models;

namespace WebAPI.DTOs
{
    public class ContentRes
    {
        public string AdditionalInfo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        
        public ContentRes(Content content)
        {
            Title = content.Name;
            Description = content.Description;
            Id = content.Id.ToString();
            Date = content.ShowDate;
            Link = content.Link;
            Image = "https://lastfm.freetls.fastly.net/i/u/ar0/251f6b43bb78fcc9d01e7cd8c20337a0.jpg";
        }
    }
}
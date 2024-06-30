using Entities.Models;

namespace WebAPI.DTOs
{
    public class ContentRes
    {
        public string AdditionalInfo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Link { get; set; }
        public string? Image { get; set; }

        public ContentRes(Content content)
        {
            Title = content.Name;
            Description = content.Description;
            Id = content.Id.ToString();
            DateStart = content.DateStart;
            DateEnd = content.DateEnd;
            Link = content.Link;
            Image = content.FileUrl;
        }
    }
}
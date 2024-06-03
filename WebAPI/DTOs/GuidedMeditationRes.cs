using Entities.Models;

namespace WebAPI.DTOs
{
    public class GuidedMeditationRes
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Audio { get; set; }
        
        public GuidedMeditationRes(GuidedMeditation guidedMeditation)
        {
            Name = guidedMeditation.Name;
            Description = guidedMeditation.Description;
            Id = guidedMeditation.Id.ToString();
            StartDate = guidedMeditation.StartDate;
            EndDate = guidedMeditation.EndDate;
            Audio = !string.IsNullOrEmpty(guidedMeditation.FileUrl) ? guidedMeditation.FileUrl : string.Empty;
        }
    }
}
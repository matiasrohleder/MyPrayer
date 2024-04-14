namespace BusinessLayer.Interfaces
{
    public interface IBibleConfiguration
    {
        public string BaseAddress { get; set; }
        public string ReadingEndpoint { get; set; }
        public string ReadingTitleEndpoint { get; set; }
        public string SaintEndpoint { get; set; }
    }
}

using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Entities.Models.Enum;
using HtmlAgilityPack;
using System.Net;

namespace BusinessLayer.BusinessLogic
{
    public class ReadingBusinessLogic : IReadingBusinessLogic
    {
        #region Properties
        private readonly IService<Reading> readingService;
        private readonly IBibleConfiguration bibleConfiguration;
        private readonly IHttpClientFactory clientFactory;
        #endregion

        #region Constructor
        public ReadingBusinessLogic(IService<Reading> readingService,
                                    IBibleConfiguration bibleConfiguration,
                                    IHttpClientFactory clientFactory)
        {
            this.readingService = readingService;
            this.bibleConfiguration = bibleConfiguration;
            this.clientFactory = clientFactory;
        }
        #endregion

        public async Task GetReadings()
        {
            DateTime date = DateTime.Today.AddDays(5);
            string dateString = date.ToString("yyyy") + date.ToString("MM") + date.ToString("dd");
            
            string baseAddress = bibleConfiguration.BaseAddress;
            string titleEndpoint = bibleConfiguration.ReadingTitleEndpoint;
            string textEndpoint = bibleConfiguration.ReadingEndpoint;

            List<ReadingEnum> readingEnums = Enum.GetValues(typeof(ReadingEnum)).Cast<ReadingEnum>().ToList();
            foreach (ReadingEnum readingEnum in readingEnums)
            {
                string content = GetContentFromEnum(readingEnum);
                string titleURL = string.Format(baseAddress + titleEndpoint, dateString, content);
                string title = await GetFromBible(titleURL);

                string textURL = string.Format(baseAddress + textEndpoint, dateString, content);
                string text = await GetFromBible(textURL);

                Reading reading = new Reading()
                {
                    Date = date,
                    Text = text,
                    Name = title,
                    ReadingEnum = readingEnum
                };

                await readingService.AddAsync(reading);
            }
        }

        private async Task<string> GetFromBible(string endpoint)
        {
            using (var client = this.clientFactory.CreateClient())
            {
                var response = await client.GetAsync(endpoint);
                var resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(resContent);

                    // Get the text without HTML formatting and decode HTML entities
                    string plainText = WebUtility.HtmlDecode(doc.DocumentNode.InnerText);

                    return plainText;
                }
                return string.Empty;
            }
        }

        private string GetContentFromEnum(ReadingEnum value)
        {
            string content = string.Empty;

            switch (value)
            {
                case ReadingEnum.FirstReading:
                    content = "FR";
                    break;
                case ReadingEnum.Psalm:
                    content = "PS";
                    break;
                case ReadingEnum.SecondReading:
                    content = "SR";
                    break;
                default:
                    content = "GSP";
                    break;
            }

            return content;
        }
    }
}
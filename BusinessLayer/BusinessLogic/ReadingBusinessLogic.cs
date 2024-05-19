using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Entities.Models.Enum;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer.BusinessLogic
{
    public class ReadingBusinessLogic(IService<Reading> readingService,
                                IBibleConfiguration bibleConfiguration,
                                IHttpClientFactory clientFactory) : IReadingBusinessLogic
    {
        #region Properties
        private readonly IService<Reading> readingService = readingService;
        private readonly IBibleConfiguration bibleConfiguration = bibleConfiguration;
        private readonly IHttpClientFactory clientFactory = clientFactory;

        #endregion
        #region Constructor
        #endregion

        public async Task GetReadings()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddDays(5);

            List<Reading> readings = await readingService.GetAll()
                                            .Where(r => r.Date >= startDate && r.Date <= endDate)
                                            .OrderBy(r => r.Date).ThenBy(r => r.ReadingEnum)
                                            .ToListAsync();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                string dateString = date.ToString("yyyy") + date.ToString("MM") + date.ToString("dd");

                List<ReadingEnum> readingEnums = Enum.GetValues(typeof(ReadingEnum)).Cast<ReadingEnum>().ToList();
                foreach (ReadingEnum readingEnum in readingEnums)
                {
                    if (readings.Any(r => r.Date.Date == date.Date && r.ReadingEnum == readingEnum))
                        continue;

                    string content = GetContentFromEnum(readingEnum);
                    string titleURL = string.Format(bibleConfiguration.BaseAddress + bibleConfiguration.ReadingTitleEndpoint, dateString, content);
                    string title = await GetFromBible(titleURL);

                    string textURL = string.Format(bibleConfiguration.BaseAddress + bibleConfiguration.ReadingEndpoint, dateString, content);
                    string text = await GetFromBible(textURL);

                    if (!string.IsNullOrWhiteSpace(text) && text.Contains("Extraído de"))
                    {
                        int index = text.IndexOf("Extraído de");
                        text = text[..index];
                    }

                    Reading reading = new()
                    {
                        Date = date,
                        Text = text,
                        Name = title,
                        ReadingEnum = readingEnum
                    };

                    await readingService.AddAsync(reading);
                }
            }
        }

        private async Task<string> GetFromBible(string endpoint)
        {
            using (var client = clientFactory.CreateClient())
            {
                var response = await client.GetAsync(endpoint);
                var resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    HtmlDocument doc = new();
                    doc.LoadHtml(resContent);

                    // Get the text without HTML formatting and decode HTML entities
                    string plainText = WebUtility.HtmlDecode(doc.DocumentNode.InnerText);

                    return plainText;
                }
                return string.Empty;
            }
        }

        private static string GetContentFromEnum(ReadingEnum value)
        {
            string content = value switch
            {
                ReadingEnum.FirstReading => "FR",
                ReadingEnum.Psalm => "PS",
                ReadingEnum.SecondReading => "SR",
                _ => "GSP",
            };
            return content;
        }
    }
}
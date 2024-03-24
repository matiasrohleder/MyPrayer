using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;

namespace BusinessLayer.BusinessLogic
{
    public class ReadingBusinessLogic : IReadingBusinessLogic
    {
        #region Properties
        private readonly IService<Reading> readingService;
        private readonly IService<Content> contentService;
        private readonly IHttpClientFactory clientFactory;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Constructor
        public ReadingBusinessLogic(
            IService<Reading> readingService,
            IService<Content> contentService,
            IHttpClientFactory clientFactory,
            IUnitOfWork unitOfWork)
        {
            this.readingService = readingService;
            this.contentService = contentService;
            this.clientFactory = clientFactory;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        public async Task Get()
        {
            var sarasa = contentService.GetAll();
        }
    }
}
namespace BusinessLayer.Interfaces
{
    public interface IReadingBusinessLogic
    {
        /// <summary>
        /// Get daily bible readings and store in DB.
        /// </summary>
        Task GetReadings();
    }
}

using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessLayer.Interfaces
{
    public interface IGuidedMeditationBusinessLogic
    {
        /// <summary>
        /// Validate guided meditation with guided meditations in DB.
        /// </summary>
        Task ValidateGuidedMeditation(GuidedMeditation guidedMeditation, ModelStateDictionary modelState);
    }
}

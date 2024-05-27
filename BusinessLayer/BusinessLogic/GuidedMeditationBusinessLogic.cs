using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System;

namespace BusinessLayer.BusinessLogic
{
    public class GuidedMeditationBusinessLogic : IGuidedMeditationBusinessLogic
    {
        #region Properties
        private readonly IService<GuidedMeditation> guidedMeditationService;
        #endregion

        #region Constructor
        public GuidedMeditationBusinessLogic(IService<GuidedMeditation> guidedMeditationService)
        {
            this.guidedMeditationService = guidedMeditationService;
        }
        #endregion

        public async Task ValidateGuidedMeditation(GuidedMeditation guidedMeditation, ModelStateDictionary modelState)
        {
            // Check for date overlap between guided meditations
            bool overlapMeditation = guidedMeditationService.GetAll().Any(gm => guidedMeditation.StartDate.ToUniversalTime() < gm.EndDate.ToUniversalTime() && 
                                                                                guidedMeditation.EndDate.ToUniversalTime() > gm.StartDate.ToUniversalTime() &&
                                                                                guidedMeditation.Id != gm.Id);

            if (overlapMeditation)
                modelState.AddModelError("endDate", "Ya existe una meditación guiada para el rango de fechas ingresado");
        }
    }
}
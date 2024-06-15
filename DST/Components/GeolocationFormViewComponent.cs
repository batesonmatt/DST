using DST.Models.Builders;
using DST.Models.DomainModels;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DST.Components
{
    public class GeolocationFormViewComponent : ViewComponent
    {
        #region Fields

        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public GeolocationFormViewComponent(IGeolocationBuilder geoBuilder)
        {
            _geoBuilder = geoBuilder;

            // Load the client geolocation, if any.
            _geoBuilder.Load();
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(Dictionary<string, string> route, string actionMethod)
        {
            string timeZoneId = _geoBuilder.CurrentGeolocation.TimeZoneId;
            string defaultId = GeolocationModel.DefaultId;

            IEnumerable<SelectListItem> timeZones = TimeZoneInfo.GetSystemTimeZones()
                .OrderByDescending(t => t.Id == defaultId)
                .ThenBy(t => t.BaseUtcOffset.TotalHours)
                .Select(t => new SelectListItem(t.DisplayName, t.Id, t.Id == timeZoneId));

            GeolocationViewModel viewModel = new()
            {
                Geolocation = _geoBuilder.CurrentGeolocation,
                TimeZones = timeZones,
                Route = route,
                ActionMethod = actionMethod
            };

            return View("~/Views/Shared/_GeolocationFormPartial.cshtml", viewModel);
        }

        #endregion
    }
}

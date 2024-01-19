using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        public IViewComponentResult Invoke(Dictionary<string, string> route)
        {
            GeolocationViewModel viewModel = new()
            {
                Geolocation = _geoBuilder.CurrentGeolocation,
                TimeZoneItems = Utilities.GetTimeZoneItems(),
                Route = route
            };

            return View("~/Views/Shared/_GeolocationFormPartial.cshtml", viewModel);
        }

        #endregion
    }
}

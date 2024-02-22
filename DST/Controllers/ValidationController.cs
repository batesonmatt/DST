using DST.Models.Builders;
using DST.Models.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DST.Controllers
{
    public class ValidationController : Controller
    {
        #region Fields

        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public ValidationController(IGeolocationBuilder geoBuilder)
        {
            _geoBuilder = geoBuilder;

            // Load the client geolocation, if any.
            _geoBuilder.Load();
        }

        #endregion

        #region Methods

        public JsonResult ValidateStartDate([Bind(Prefix = "TrackForm.Start")] DateTime start)
        {
            string message = Utilities.ValidateClientDateTime(start, _geoBuilder.CurrentGeolocation);

            return string.IsNullOrWhiteSpace(message) ? Json(true) : Json(message);
        }

        #endregion
    }
}

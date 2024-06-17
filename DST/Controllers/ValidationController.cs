using DST.Models.Builders;
using DST.Models.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DST.Controllers
{
    public class ValidationController : Controller
    {
        #region Methods

        public JsonResult ValidateStartDate(
            [FromServices] IGeolocationBuilder geoBuilder,
            [Bind(Prefix = "TrackForm.Start")] DateTime start)
        {
            // Load the client geolocation, if any.
            geoBuilder.Load();
            string message = Utilities.ValidateClientDateTime(start, geoBuilder.CurrentGeolocation);
            return string.IsNullOrWhiteSpace(message) ? Json(true) : Json(message);
        }

        public JsonResult ValidatePeriod(
            [Bind(Prefix = "TrackForm.Period")] int period,
            [Bind(Prefix = "TrackForm.Algorithm")] string algorithm,
            [Bind(Prefix = "TrackForm.TimeUnit")] string timeUnit,
            [Bind(Prefix = "TrackForm.IsFixed")] bool isFixed)
        {
            string message = Utilities.ValidateClientPeriod(period, algorithm, timeUnit, isFixed);
            return string.IsNullOrWhiteSpace(message) ? Json(true) : Json(message);
        }

        public JsonResult ValidateInterval(
            [Bind(Prefix = "TrackForm.Interval")] int interval,
            [Bind(Prefix = "TrackForm.Period")] int period)
        {
            string message = Utilities.ValidateClientInterval(interval, period);
            return string.IsNullOrWhiteSpace(message) ? Json(true) : Json(message);
        }

        #endregion
    }
}

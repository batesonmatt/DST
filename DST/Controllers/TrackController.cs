using DST.Core.Observer;
using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Repositories;
using DST.Models.DomainModels;
using DST.Models.Routes;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DST.Controllers
{
    public class TrackController : Controller
    {
        #region Fields

        private readonly IRepository<DsoModel> _data;
        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public TrackController(IRepository<DsoModel> data, IGeolocationBuilder geoBuilder)
        {
            _data = data;
            _geoBuilder = geoBuilder;

            // Load the client geolocation, if any.
            _geoBuilder.Load();
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            // Redirect to the Summary endpoint, with default route segments.
            return RedirectToAction("Summary");
        }

        [HttpPost]
        public IActionResult SubmitGeolocation(GeolocationModel geolocation, TrackSummaryRoute values, bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Summary", values.ToDictionary());
            }

            // Set the location coordinates.
            _geoBuilder.CurrentGeolocation.Latitude = geolocation.Latitude;
            _geoBuilder.CurrentGeolocation.Longitude = geolocation.Longitude;

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                _geoBuilder.CurrentGeolocation.Reset();
            }
            else if (geolocation.TimeZoneId != string.Empty)
            {
                // Verify the selected id.
                _geoBuilder.CurrentGeolocation.VerifyAndUpdateTimeZone(geolocation.TimeZoneId);
            }
            else if (geolocation.UserTimeZoneId != string.Empty)
            {
                // Try to verify the retrieved IANA id.
                _geoBuilder.CurrentGeolocation.VerifyAndUpdateTimeZone(geolocation.UserTimeZoneId);
            }
            else
            {
                // No timezone was selected or found. Default to UTC.
                _geoBuilder.CurrentGeolocation.ResetTimeZone();
            }

            // Save the geolocation in session and create a persistent cookie.
            _geoBuilder.Save();

            return RedirectToAction("Summary", values.ToDictionary());
        }

        public ViewResult Summary(TrackSummaryRoute values)
        {
            // Validate route values.
            values.Validate();

            GeolocationModel geolocation = _geoBuilder.CurrentGeolocation;
            DsoModel dso = _data.Get(values.Catalog, values.Id);

            /* Allow the client to choose the algorithm by using a separate HttpPost action that redirects to Summary(). */
            IObserver observer = Utilities.GetObserver(dso, geolocation, Core.TimeKeeper.Algorithm.GMST);

            TrackViewModel viewModel = new()
            {
                Geolocation = geolocation,
                TimeZoneItems = Utilities.GetTimeZoneItems(),
                Dso = dso,
                ClientObserver = observer,
                CurrentRoute = values
            };

            return View(viewModel);
            //return RedirectToAction("Summary", values.ToDictionary());
        }

        /* Takes TrackPhaseRoute */
        public ViewResult Phase()
        {
            /* Create TrackViewModel and pass into View(model) */
            return View();
        }

        /* Takes TrackPeriodRoute */
        public ViewResult Period()
        {
            /* Create TrackViewModel and pass into View(model) */
            return View();
        }

        #endregion
    }
}

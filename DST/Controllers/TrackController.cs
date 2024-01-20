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

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                _geoBuilder.CurrentGeolocation.Reset();
            }
            else
            {
                // Update geolocation and timezone.
                _geoBuilder.CurrentGeolocation.SetGeolocation(geolocation);
            }

            // Save the geolocation in session and create a persistent cookie.
            _geoBuilder.Save();

            return RedirectToAction("Summary", values.ToDictionary());
        }

        public ViewResult Summary(TrackSummaryRoute values)
        {
            // Validate route values.
            values.Validate();

            DsoModel dso = _data.Get(values.Catalog, values.Id);

            /* Allow the client to choose the algorithm by using a separate HttpPost action that redirects to Summary(). */
            ILocalObserver localObserver = 
                Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, Core.TimeKeeper.Algorithm.GMST);

            TrackViewModel viewModel = new()
            {
                Dso = dso,
                ClientObserver = localObserver,
                CurrentRoute = values
            };

            return View(viewModel);
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

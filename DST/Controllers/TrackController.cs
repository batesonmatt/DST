using DST.Core.Observer;
using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.DataLayer.Repositories;
using DST.Models.DomainModels;
using DST.Models.Extensions;
using DST.Models.Routes;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System.Collections.Generic;

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
        public IActionResult SubmitSummaryGeolocation(GeolocationModel geolocation, TrackSummaryRoute values, bool reset = false)
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

        [HttpPost]
        public IActionResult SubmitPhaseGeolocation(GeolocationModel geolocation, TrackPhaseRoute values, bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Phase", values.ToDictionary());
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

            return RedirectToAction("Phase", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult SetAlgorithm(/* route values here, */ string algorithm)
        {
            return View();
        }

        [HttpGet]
        public ViewResult Summary(TrackSummaryRoute values)
        {
            // Validate route values.
            values.Validate();

            DsoModel dso = _data.Get(values.Catalog, values.Id);

            /* Allow the client to choose the algorithm by using a separate HttpPost action that redirects to Summary(TrackSummaryRoute). */
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

        [HttpPost]
        public IActionResult SubmitPhase(TrackPhaseModel phaseModel, TrackPhaseRoute values)
        {
            /* Test this. */
            // Check model state.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Phase", values.ToDictionary());
            }

            /* perform validation */

            values.SetPhase(phaseModel.Phase);
            values.SetStart(phaseModel.GetTicks());
            values.SetCycles(phaseModel.Cycles);

            /* save to session state */

            return RedirectToAction("Phase", values.ToDictionary());
        }

        [HttpGet]
        public ViewResult Phase(TrackPhaseRoute values)
        {
            // Validate route values.
            values.Validate();

            DsoModel dso = _data.Get(values.Catalog, values.Id);

            /* Allow the client to choose the algorithm by using a separate HttpPost action that redirects to POST Phase(TrackPhaseRoute). */
            ILocalObserver localObserver =
                Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, Core.TimeKeeper.Algorithm.GMST);

            TrackPhaseViewModel viewModel = new()
            {
                Dso = dso,
                ClientObserver = localObserver,
                CurrentRoute = values,

                PhaseModel = new TrackPhaseModel()
                {
                    Phase = values.Phase,
                    Start = values.Start == 0 ? null : values.Start.ToDateTime(), /* I want to default this to null so that the input control displays the cleared format. */
                    Cycles = values.Cycles
                },

                Phases = new List<TrackPhaseItem> {
                    new TrackPhaseItem(PhaseName.Rise.ToKebabCase(), PhaseName.Rise),
                    new TrackPhaseItem(PhaseName.Apex.ToKebabCase(), PhaseName.Apex),
                    new TrackPhaseItem(PhaseName.Set.ToKebabCase(), PhaseName.Set)
                }
            };

            return View(viewModel);
        }

        /* Takes TrackPeriodRoute */
        public ViewResult Period()
        {
            /* Create TrackPeriodViewModel and pass into View(viewModel) */
            return View();
        }

        #endregion
    }
}

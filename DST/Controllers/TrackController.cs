using DST.Core.Observer;
using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Repositories;
using DST.Models.DomainModels;
using DST.Models.Extensions;
using DST.Models.Routes;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DST.Core.Trajectory;
using DST.Core.TimeKeeper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DST.Controllers
{
    public class TrackController : Controller
    {
        #region Methods

        public IActionResult Index()
        {
            // Redirect to the Summary endpoint, with default route segments.
            return RedirectToAction("Summary");
        }

        [HttpPost]
        public IActionResult SubmitSummaryGeolocation(
            [FromServices] IGeolocationBuilder geoBuilder,
            GeolocationModel geolocation,
            TrackSummaryRoute values,
            bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("Summary", values.ToDictionary());
            }

            // Load the client geolocation, if any.
            geoBuilder.Load();

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                geoBuilder.CurrentGeolocation.Reset();
            }
            else
            {
                // Update geolocation and timezone.
                geoBuilder.CurrentGeolocation.SetGeolocation(geolocation);
            }

            // Save the geolocation in session and create a persistent cookie.
            geoBuilder.Save();

            return RedirectToAction("Summary", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult SubmitPhaseGeolocation(
            [FromServices] IGeolocationBuilder geoBuilder,
            GeolocationModel geolocation,
            TrackPhaseRoute values,
            bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("Phase", values.ToDictionary());
            }

            // Load the client geolocation, if any.
            geoBuilder.Load();

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                geoBuilder.CurrentGeolocation.Reset();
            }
            else
            {
                // Update geolocation and timezone.
                geoBuilder.CurrentGeolocation.SetGeolocation(geolocation);
            }

            // Save the geolocation in session and create a persistent cookie.
            geoBuilder.Save();

            return RedirectToAction("Phase", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult SubmitPeriodGeolocation(
            [FromServices] IGeolocationBuilder geoBuilder,
            GeolocationModel geolocation,
            TrackPeriodRoute values,
            bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("Period", values.ToDictionary());
            }

            // Load the client geolocation, if any.
            geoBuilder.Load();

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                geoBuilder.CurrentGeolocation.Reset();
            }
            else
            {
                // Update geolocation and timezone.
                geoBuilder.CurrentGeolocation.SetGeolocation(geolocation);
            }

            // Save the geolocation in session and create a persistent cookie.
            geoBuilder.Save();

            return RedirectToAction("Period", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult SetAlgorithm(TrackSummaryRoute values, string algorithm)
        {
            values.SetAlgorithm(algorithm);

            return RedirectToAction("Summary", values.ToDictionary());
        }

        [HttpGet]
        public ViewResult Summary(
            [FromServices] ITrackUnitOfWork data,
            [FromServices] IGeolocationBuilder geoBuilder,
            TrackSummaryRoute values)
        {
            // Validate route values.
            values.Validate();

            // Load the client geolocation, if any.
            geoBuilder.Load();

            DsoModel dso = data.DsoItems.Get(values.Catalog, values.Id);
            SeasonModel season = data.GetSeason(dso);
            ConstellationModel constellation = data.GetConstellation(dso);
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);

            IEnumerable<SelectListItem> algorithms = AlgorithmName.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Algorithm));

            TrackSummaryViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                SummaryInfo = Utilities.GetSummaryInfo(dso, season, constellation, geoBuilder.CurrentGeolocation, algorithm),
                Algorithms = algorithms
            };

            return View(viewModel);
        }

        private static TrackPhaseViewModel GetPhaseViewModel(
            ITrackUnitOfWork data,
            IGeolocationBuilder geoBuilder,
            ITrackPhaseBuilder phaseBuilder,
            TrackPhaseRoute values,
            bool buildResults)
        {
            // Validate route values.
            values.Validate();

            // Load the client geolocation, if any.
            geoBuilder.Load();

            DsoModel dso = data.DsoItems.Get(values.Catalog, values.Id);
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);
            ILocalObserver localObserver = Utilities.GetLocalObserver(dso, geoBuilder.CurrentGeolocation, algorithm);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(localObserver);

            TrackResults results = new();
            IEnumerable<SelectListItem> phases = System.Array.Empty<SelectListItem>();
            string selectedPhase = string.Empty;
            string message = string.Empty;

            if (trajectory is IRiseSetTrajectory)
            {
                selectedPhase = values.Phase;

                phases = PhaseName.GetTextValuePairs().Select(
                    i => new SelectListItem(i.Text, i.Value, i.Value == selectedPhase));

            }
            else if (trajectory is ICircumpolarTrajectory and not IVariableTrajectory)
            {
                message = Resources.DisplayText.TrackPhaseWarningCircumpolar;
            }
            else if (trajectory is ICircumpolarTrajectory and IVariableTrajectory)
            {
                message = Resources.DisplayText.TrackPhaseWarningCircumpolarOffset;
                selectedPhase = PhaseName.Apex.ToKebabCase();

                phases = PhaseName.GetTextValuePairs().Select(
                    i => new SelectListItem(i.Text, i.Value, i.Value == selectedPhase, i.Value != selectedPhase));
            }
            else
            {
                message = Resources.DisplayText.TrackPhaseWarningNeverRise;
            }

            if (buildResults)
            {
                // Load the previous phase entry, if any.
                phaseBuilder.Load();

                // Calculate the phase tracking results if an entry was submitted.
                if (phaseBuilder.Current.IsReady)
                {
                    results = Utilities.GetPhaseResults(localObserver, phaseBuilder.Current);

                    // Force the client to resubmit the form.
                    phaseBuilder.Current.IsReady = false;
                    phaseBuilder.Save();
                }
            }

            IEnumerable<SelectListItem> algorithms = AlgorithmName.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Algorithm));

            TrackPhaseViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                Algorithms = algorithms,
                Phases = phases,

                TrackForm = new TrackPhaseModel()
                {
                    Algorithm = values.Algorithm,
                    Start = Utilities.GetClientDateTime(geoBuilder.CurrentGeolocation, values.Start),
                    Phase = selectedPhase,
                    IsTrackOnce = values.IsTrackOnce,
                    Cycles = values.Cycles
                },

                Results = results,
                WarningMessage = message
            };

            return viewModel;
        }

        [HttpPost]
        public IActionResult SubmitPhase(
            [FromServices] ITrackUnitOfWork data,
            [FromServices] IGeolocationBuilder geoBuilder,
            [FromServices] ITrackPhaseBuilder phaseBuilder,
            TrackPhaseModel trackForm,
            TrackPhaseRoute values)
        {
            // Check server-side validation state.
            if (!ModelState.IsValid)
            {
                // Re-render the view so that server-side validation messages are displayed.
                return View("Phase", GetPhaseViewModel(data, geoBuilder, phaseBuilder, values, buildResults: false));
            }

            values.SetAlgorithm(trackForm.Algorithm);
            values.SetStart(trackForm.GetTicks());
            values.SetPhase(trackForm.Phase);
            values.SetTrackOnce(trackForm.IsTrackOnce);
            values.SetCycles(trackForm.Cycles);

            // Mark the entry as ready so we can calculate the results.
            trackForm.IsReady = true;

            // Set the current phase entry.
            phaseBuilder.Current = trackForm;

            // Save the phase entry to session state.
            phaseBuilder.Save();

            // Redirect the action to calculate the results.
            return RedirectToAction("Phase", values.ToDictionary());
        }

        [HttpGet]
        public ViewResult Phase(
            [FromServices] ITrackUnitOfWork data,
            [FromServices] IGeolocationBuilder geoBuilder,
            [FromServices] ITrackPhaseBuilder phaseBuilder,
            TrackPhaseRoute values)
        {
            return View(GetPhaseViewModel(data, geoBuilder, phaseBuilder, values, buildResults: true));
        }

        private static TrackPeriodViewModel GetPeriodViewModel(
            ITrackUnitOfWork data,
            IGeolocationBuilder geoBuilder,
            ITrackPeriodBuilder periodBuilder,
            TrackPeriodRoute values,
            bool buildResults)
        {
            // Validate route values.
            values.Validate();

            // Load the client geolocation, if any.
            geoBuilder.Load();

            DsoModel dso = data.DsoItems.Get(values.Catalog, values.Id);
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);
            ILocalObserver localObserver = Utilities.GetLocalObserver(dso, geoBuilder.CurrentGeolocation, algorithm);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(localObserver);

            TrackResults results = new();
            string message = string.Empty;

            if (trajectory is NeverRiseTrajectory)
            {
                message = Resources.DisplayText.TrackPeriodWarningNeverRise;
            }
            else if (trajectory is ICircumpolarTrajectory)
            {
                message = Resources.DisplayText.TrackPeriodWarningCircumpolar;
            }

            if (buildResults)
            {
                // Load the previous period entry, if any.
                periodBuilder.Load();

                // Calculate the period tracking results if an entry was submitted.
                if (periodBuilder.Current.IsReady)
                {
                    results = Utilities.GetPeriodResults(localObserver, algorithm, periodBuilder.Current);

                    // Force the client to resubmit the form.
                    periodBuilder.Current.IsReady = false;
                    periodBuilder.Save();
                }
            }

            IEnumerable<SelectListItem> algorithms = AlgorithmName.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Algorithm));

            IEnumerable<SelectListItem> timeUnits = TimeUnitName.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.TimeUnit));

            TrackPeriodViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                Algorithms = algorithms,
                TimeUnits = timeUnits,

                TrackForm = new TrackPeriodModel()
                {
                    Algorithm = values.Algorithm,
                    Start = Utilities.GetClientDateTime(geoBuilder.CurrentGeolocation, values.Start),
                    IsTrackOnce = values.IsTrackOnce,
                    IsFixed = values.IsFixed,
                    IsAggregated = values.IsAggregated,
                    TimeUnit = values.TimeUnit,
                    Period = values.Period,
                    Interval = values.Interval
                },

                Results = results,
                WarningMessage = message
            };

            return viewModel;
        }

        [HttpPost]
        public IActionResult SubmitPeriod(
            [FromServices] ITrackUnitOfWork data,
            [FromServices] IGeolocationBuilder geoBuilder,
            [FromServices] ITrackPeriodBuilder periodBuilder,
            TrackPeriodModel trackForm,
            TrackPeriodRoute values)
        {
            // Check server-side validation state.
            if (!ModelState.IsValid)
            {
                // Re-render the view so that server-side validation messages are displayed.
                return View("Period", GetPeriodViewModel(data, geoBuilder, periodBuilder, values, buildResults: false));
            }

            // The ordering is important here.
            values.SetAlgorithm(trackForm.Algorithm);
            values.SetStart(trackForm.GetTicks());
            values.SetTrackOnce(trackForm.IsTrackOnce);
            values.SetTimeUnit(trackForm.TimeUnit);
            values.SetFixed(trackForm.IsFixed);
            values.SetAggregate(trackForm.IsAggregated);
            values.SetPeriod(trackForm.Period);
            values.SetInterval(trackForm.Interval);

            // Mark the entry as ready so we can calculate the results.
            trackForm.IsReady = true;

            // Set the current period entry.
            periodBuilder.Current = trackForm;

            // Save the period entry to session state.
            periodBuilder.Save();

            // Redirect the action to calculate the results.
            return RedirectToAction("Period", values.ToDictionary());
        }

        public ViewResult Period(
            [FromServices] ITrackUnitOfWork data,
            [FromServices] IGeolocationBuilder geoBuilder,
            [FromServices] ITrackPeriodBuilder periodBuilder,
            TrackPeriodRoute values)
        {
            return View(GetPeriodViewModel(data, geoBuilder, periodBuilder, values, buildResults: true));
        }

        #endregion
    }
}

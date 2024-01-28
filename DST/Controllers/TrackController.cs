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
using DST.Core.Components;
using System.Globalization;
using DST.Core.Coordinate;
using System;
using DST.Core.DateAndTime;
using DST.Core.Tracker;
using DST.Models.DataLayer;

namespace DST.Controllers
{
    public class TrackController : Controller
    {
        #region Fields

        private readonly TrackUnitOfWork _data;
        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public TrackController(MainDbContext context, IGeolocationBuilder geoBuilder)
        {
            _data = new TrackUnitOfWork(context);
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

            DsoModel dso = _data.DsoItems.Get(values.Catalog, values.Id);

            /* Allow the client to choose the algorithm by using a separate HttpPost action that redirects to Summary(TrackSummaryRoute). */
            ILocalObserver localObserver = 
                Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, Core.TimeKeeper.Algorithm.GMST);

            SeasonModel season = _data.GetSeason(dso);

            IMutableDateTime clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, localObserver.DateTimeInfo);
            IAstronomicalDateTime astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);
            ITracker tracker = TrackerFactory.Create(localObserver);
            IHorizontalCoordinate position = tracker.Track(astronomicalDateTime) as IHorizontalCoordinate;

            Dictionary<string, string> info = new()
            {
                { Resources.DisplayText.TargetRightAscensionLong, localObserver.Target.Format(FormatType.Component, ComponentType.Rotation) },
                { Resources.DisplayText.TargetDeclinationLong, localObserver.Target.Format(FormatType.Component, ComponentType.Inclination) },
                { Resources.DisplayText.TargetCatalog, dso.CatalogName },
                { Resources.DisplayText.TargetType, dso.Type },
                { Resources.DisplayText.TargetDescription, dso.Description },
                { Resources.DisplayText.TargetConstellation, dso.ConstellationName },
                { Resources.DisplayText.TargetDistance, string.Format(Resources.DisplayText.DistanceFormatDecimalKly, dso.Distance) },
                { Resources.DisplayText.TargetMagnitude, dso.Magnitude?.ToString(CultureInfo.CurrentCulture) ?? Resources.DisplayText.None },
                {
                    Resources.DisplayText.TargetSeason, 
                    string.Format(Resources.DisplayText.TargetSeasonDetailsFormat,
                        localObserver.Location.Latitude >= 0.0 ? season.North : season.South,
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(season.StartMonth),
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(season.EndMonth))
                },

                { Resources.DisplayText.ObserverLatitudeLong, localObserver.Location.Format(FormatType.Component, ComponentType.Inclination) },
                { Resources.DisplayText.ObserverLongitudeLong, localObserver.Location.Format(FormatType.Component, ComponentType.Rotation) },
                { Resources.DisplayText.ObserverTimeZone, localObserver.DateTimeInfo.ClientTimeZoneInfo.DisplayName },
                { Resources.DisplayText.ObserverLocalTime, clientDateTime.ToLocalTime().ToString(CultureInfo.CurrentCulture) },
                { Resources.DisplayText.ObserverUniversalTimeLong, clientDateTime.Value.ToString(CultureInfo.CurrentCulture) },

                { Resources.DisplayText.ObserverTimeKeeper, localObserver.TimeKeeper.ToString() },
                { localObserver.TimeKeeper.ToString(), localObserver.TimeKeeper.Calculate(astronomicalDateTime).ToString() },
                { localObserver.LocalTimeKeeper.ToString(), localObserver.LocalTimeKeeper.Calculate(localObserver, astronomicalDateTime).ToString() },
                { localObserver.LocalHourAngle.ToString(), localObserver.LocalHourAngle.Calculate(localObserver, astronomicalDateTime).ToString() },
                { Resources.DisplayText.TargetAltitudeLong, position.Format(FormatType.Component, ComponentType.Inclination) },
                { Resources.DisplayText.TargetAzimuthLong, position.Format(FormatType.Component, ComponentType.Rotation) },
                { Resources.DisplayText.TargetTrajectory, Utilities.GetPrimaryTrajectoryName(dso, _geoBuilder.CurrentGeolocation) },
                { Resources.DisplayText.TargetLocal, ""}
            };

            TrackSummaryViewModel viewModel = new()
            {
                Dso = dso,
                ClientObserver = localObserver,
                CurrentRoute = values,
                DisplayInfo = info
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

            DsoModel dso = _data.DsoItems.Get(values.Catalog, values.Id);

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

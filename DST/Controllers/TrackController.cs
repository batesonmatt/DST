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
using DST.Core.Trajectory;

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
        public IActionResult SetAlgorithm(TrackSummaryRoute values, string algorithm)
        {
            values.SetAlgorithm(algorithm);

            return RedirectToAction("Summary", values.ToDictionary());
        }

        [HttpGet]
        public ViewResult Summary(TrackSummaryRoute values)
        {
            // Validate route values.
            values.Validate();

            DsoModel dso = _data.DsoItems.Get(values.Catalog, values.Id);

            Core.TimeKeeper.Algorithm algorithm;

            if (values.Algorithm.EqualsSeo(AlgorithmName.GMST))
            {
                algorithm = Core.TimeKeeper.Algorithm.GMST;
            }
            else if (values.Algorithm.EqualsSeo(AlgorithmName.GAST))
            {
                algorithm = Core.TimeKeeper.Algorithm.GAST;
            }
            else if (values.Algorithm.EqualsSeo(AlgorithmName.ERA))
            {
                algorithm= Core.TimeKeeper.Algorithm.ERA;
            }
            else
            {
                algorithm = Core.TimeKeeper.Algorithm.Default;
            }

            ILocalObserver localObserver = 
                Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, algorithm);

            SeasonModel season = _data.GetSeason(dso);
            ConstellationModel constellation = _data.GetConstellation(dso);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(localObserver);
            IMutableDateTime clientDateTime = DateTimeFactory.CreateMutable(DateTime.UtcNow, localObserver.DateTimeInfo);
            DateTime clientLocalTime = clientDateTime.ToLocalTime();
            IAstronomicalDateTime astronomicalDateTime = DateTimeFactory.ConvertToAstronomical(clientDateTime);
            ITracker tracker = TrackerFactory.Create(localObserver);
            IHorizontalCoordinate position = tracker.Track(astronomicalDateTime) as IHorizontalCoordinate;

            string visibility;

            if (trajectory is null or NeverRiseTrajectory)
            {
                visibility = Resources.DisplayText.TargetVisibilityNeverRise;
            }
            else if (trajectory is ICircumpolarTrajectory)
            {
                visibility = Resources.DisplayText.TargetVisibilityCircumpolar;
            }
            else if (localObserver.Location.Latitude > constellation.NorthernLatitude ||
                     localObserver.Location.Latitude < -constellation.SouthernLatitude)
            {
                visibility = Resources.DisplayText.TargetVisibilityOutOfRange;
            }
            else if (!season.ContainsDate(clientLocalTime))
            {
                visibility = Resources.DisplayText.TargetVisibilityOutOfSeason;
            }
            else
            {
                visibility = Resources.DisplayText.TargetVisibilityInRange;
            }

            TrackSummaryInfo info = new()
            {
                { TrackSummaryItem.RightAscension, new(Resources.DisplayText.TargetRightAscensionLong, localObserver.Target.Format(FormatType.Component, ComponentType.Rotation)) },
                { TrackSummaryItem.Declination, new(Resources.DisplayText.TargetDeclinationLong, localObserver.Target.Format(FormatType.Component, ComponentType.Inclination)) },
                { TrackSummaryItem.Catalog, new(Resources.DisplayText.TargetCatalog, dso.CatalogName) },
                { TrackSummaryItem.Type, new(Resources.DisplayText.TargetType, dso.Type) },
                { TrackSummaryItem.Description, new(Resources.DisplayText.TargetDescription, dso.Description) },
                { TrackSummaryItem.Constellation, new(Resources.DisplayText.TargetConstellation, dso.ConstellationName) },
                { TrackSummaryItem.Distance, new(Resources.DisplayText.TargetDistance, string.Format(Resources.DisplayText.DistanceFormatDecimalKly, dso.Distance)) },
                { TrackSummaryItem.Magnitude, new(Resources.DisplayText.TargetMagnitude, dso.Magnitude ?.ToString(CultureInfo.CurrentCulture) ?? Resources.DisplayText.None) },
                {
                    TrackSummaryItem.Season,
                    new(Resources.DisplayText.TargetSeason, 
                        string.Format(Resources.DisplayText.TargetSeasonDetailsFormat,
                            localObserver.Location.Latitude >= 0.0 ? season.North : season.South,
                            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(season.StartMonth),
                            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(season.EndMonth)))
                },
                { TrackSummaryItem.Latitude, new(Resources.DisplayText.ObserverLatitudeLong, localObserver.Location.Format(FormatType.Component, ComponentType.Inclination)) },
                { TrackSummaryItem.Longitude, new(Resources.DisplayText.ObserverLongitudeLong, localObserver.Location.Format(FormatType.Component, ComponentType.Rotation)) },
                { TrackSummaryItem.TimeZone, new(Resources.DisplayText.ObserverTimeZone, localObserver.DateTimeInfo.ClientTimeZoneInfo.DisplayName) },
                { TrackSummaryItem.LocalTime, new(Resources.DisplayText.ObserverLocalTime, clientLocalTime.ToString(CultureInfo.CurrentCulture)) },
                { TrackSummaryItem.UniversalTime, new(Resources.DisplayText.ObserverUniversalTimeLong, clientDateTime.Value.ToString(CultureInfo.CurrentCulture)) },
                { TrackSummaryItem.Algorithm, new(Resources.DisplayText.ObserverAlgorithm, string.Empty) },
                { TrackSummaryItem.TimeKeeper, new(localObserver.TimeKeeper.ToString(), localObserver.TimeKeeper.Calculate(astronomicalDateTime).ToString()) },
                { TrackSummaryItem.LocalTimeKeeper, new(localObserver.LocalTimeKeeper.ToString(), localObserver.LocalTimeKeeper.Calculate(localObserver, astronomicalDateTime).ToString()) },
                { TrackSummaryItem.LocalHourAngle, new(localObserver.LocalHourAngle.ToString(), localObserver.LocalHourAngle.Calculate(localObserver, astronomicalDateTime).ToString()) },
                { TrackSummaryItem.Altitude, new(Resources.DisplayText.TargetAltitudeLong, position.Format(FormatType.Component, ComponentType.Inclination)) },
                { TrackSummaryItem.Azimuth, new(Resources.DisplayText.TargetAzimuthLong, position.Format(FormatType.Component, ComponentType.Rotation)) },
                { TrackSummaryItem.Trajectory, new(Resources.DisplayText.TargetTrajectory, Utilities.GetPrimaryTrajectoryName(trajectory)) },
                { TrackSummaryItem.Visibility, new(Resources.DisplayText.TargetVisibility, visibility) }
            };

            TrackSummaryViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                DisplayInfo = info,
                Algorithms = Utilities.GetAlgorithmItems()
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

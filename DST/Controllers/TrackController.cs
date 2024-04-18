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
using DST.Core.Components;
using System.Globalization;
using DST.Core.Coordinate;
using System;
using DST.Core.DateAndTime;
using DST.Core.Tracker;
using DST.Models.DataLayer;
using DST.Core.Trajectory;
using DST.Core.TimeKeeper;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using DST.Core.TimeScalable;
using DST.Core.DateTimeAdder;
using DST.Core.DateTimesBuilder;

namespace DST.Controllers
{
    public class TrackController : Controller
    {
        #region Fields

        private readonly TrackUnitOfWork _data;
        private readonly IGeolocationBuilder _geoBuilder;
        private readonly ITrackPhaseBuilder _phaseBuilder;
        private readonly ITrackPeriodBuilder _periodBuilder;

        #endregion

        #region Constructors

        public TrackController(MainDbContext context, IGeolocationBuilder geoBuilder, ITrackPhaseBuilder phaseBuilder, ITrackPeriodBuilder periodBuilder)
        {
            _data = new TrackUnitOfWork(context);
            _geoBuilder = geoBuilder;
            _phaseBuilder = phaseBuilder;
            _periodBuilder = periodBuilder;

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
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
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
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
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
        public IActionResult SubmitPeriodGeolocation(GeolocationModel geolocation, TrackPeriodRoute values, bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("Period", values.ToDictionary());
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

            return RedirectToAction("Period", values.ToDictionary());
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
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);
            ILocalObserver localObserver = Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, algorithm);
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

        private TrackPhaseViewModel GetPhaseViewModel(TrackPhaseRoute values, bool buildResults)
        {
            // Validate route values.
            values.Validate();

            DsoModel dso = _data.DsoItems.Get(values.Catalog, values.Id);
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);
            Phase phase = Utilities.GetPhase(values.Phase);
            ILocalObserver localObserver = Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, algorithm);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(localObserver);

            IEnumerable<TrackPhaseResult> results = Enumerable.Empty<TrackPhaseResult>();
            List<SelectListItem> phases = new();
            string selectedPhase = string.Empty;
            string message = string.Empty;

            if (trajectory is IRiseSetTrajectory)
            {
                selectedPhase = values.Phase;

                phases.Add(new SelectListItem(PhaseName.Rise, PhaseName.Rise.ToKebabCase(), PhaseName.Rise.ToKebabCase() == selectedPhase));
                phases.Add(new SelectListItem(PhaseName.Apex, PhaseName.Apex.ToKebabCase(), PhaseName.Apex.ToKebabCase() == selectedPhase));
                phases.Add(new SelectListItem(PhaseName.Set, PhaseName.Set.ToKebabCase(), PhaseName.Set.ToKebabCase() == selectedPhase));
            }
            else if (trajectory is ICircumpolarTrajectory and not IVariableTrajectory)
            {
                message = Resources.DisplayText.TrackPhaseWarningCircumpolar;
            }
            else if (trajectory is ICircumpolarTrajectory and IVariableTrajectory)
            {
                message = Resources.DisplayText.TrackPhaseWarningCircumpolarOffset;

                selectedPhase = PhaseName.Apex.ToKebabCase();

                phases.Add(new SelectListItem(PhaseName.Rise, PhaseName.Rise.ToKebabCase(), selected: false, disabled: true));
                phases.Add(new SelectListItem(PhaseName.Apex, PhaseName.Apex.ToKebabCase(), selected: true));
                phases.Add(new SelectListItem(PhaseName.Set, PhaseName.Set.ToKebabCase(), selected: false, disabled: true));
            }
            else
            {
                message = Resources.DisplayText.TrackPhaseWarningNeverRise;
            }

            if (buildResults)
            {
                // Load the previous phase entry, if any.
                _phaseBuilder.Load();

                // Calculate the phase tracking results if an entry was submitted.
                if (_phaseBuilder.Current.IsReady)
                {
                    IAstronomicalDateTime start = DateTimeFactory.CreateAstronomical(_phaseBuilder.Current.Start, localObserver.DateTimeInfo);

                    results = Utilities.GetPhaseResults(trajectory, phase, start, _phaseBuilder.Current.Cycles);

                    // Force the client to resubmit the form.
                    _phaseBuilder.Current.IsReady = false;
                    _phaseBuilder.Save();
                }
            }

            TrackPhaseViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                Algorithms = Utilities.GetAlgorithmItems(),
                Phases = phases,

                TrackForm = new TrackPhaseModel()
                {
                    Algorithm = values.Algorithm,
                    Phase = selectedPhase,
                    Start = Utilities.GetClientDateTime(_geoBuilder.CurrentGeolocation, values.Start),
                    Cycles = values.Cycles
                },

                Results = results,
                WarningMessage = message
            };

            return viewModel;
        }

        [HttpPost]
        public IActionResult SubmitPhase(TrackPhaseModel trackForm, TrackPhaseRoute values)
        {
            // Check server-side validation state.
            if (!ModelState.IsValid)
            {
                // Re-render the view so that server-side validation messages are displayed.
                return View("Phase", GetPhaseViewModel(values, buildResults: false));
            }

            values.SetAlgorithm(trackForm.Algorithm);
            values.SetPhase(trackForm.Phase);
            values.SetStart(trackForm.GetTicks());
            values.SetCycles(trackForm.Cycles);

            // Mark the entry as ready so we can calculate the results.
            trackForm.IsReady = true;

            // Set the current phase entry.
            _phaseBuilder.Current = trackForm;

            // Save the phase entry to session state.
            _phaseBuilder.Save();

            // Redirect the action to calculate the results.
            return RedirectToAction("Phase", values.ToDictionary());
        }

        [HttpGet]
        public ViewResult Phase(TrackPhaseRoute values)
        {
            return View(GetPhaseViewModel(values, buildResults: true));
        }

        private TrackPeriodViewModel GetPeriodViewModel(TrackPeriodRoute values, bool buildResults)
        {
            // Validate route values.
            values.Validate();

            DsoModel dso = _data.DsoItems.Get(values.Catalog, values.Id);
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);
            ILocalObserver localObserver = Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, algorithm);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(localObserver);

            //IEnumerable<TrackPeriodResult> results = Enumerable.Empty<TrackPeriodResult>();
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
                _periodBuilder.Load();

                // Calculate the period tracking results if an entry was submitted.
                if (_periodBuilder.Current.IsReady)
                {
                    IAstronomicalDateTime start = DateTimeFactory.CreateAstronomical(_periodBuilder.Current.Start, localObserver.DateTimeInfo);

                    //results = Utilities.GetPeriodResults(
                    //    trajectory, start,
                    //    _periodBuilder.Current.TimeUnit,
                    //    _periodBuilder.Current.Period,
                    //    _periodBuilder.Current.Interval,
                    //    _periodBuilder.Current.IsFixed);

                    // Get TimeScale
                    TimeScale timeScale = Utilities.GetTimeScale(algorithm, values.IsFixed);
                    // Get TimeUnit
                    TimeUnit timeUnit = Utilities.GetTimeUnit(values.TimeUnit);
                    // Get DateTimeAdder
                    IDateTimeAdder dateTimeAdder = Utilities.GetDateTimeAdder(timeScale, timeUnit);
                    // Get DateTimesBuilder
                    /* New option - Aggregate/IsAggregated
                     * (Only available for fixed tracking, Months and Years)
                     * If true, then add from starting datetime
                     * Else, add from previous interval
                     */
                    IDateTimesBuilder dateTimesBuilder = DateTimesBuilderFactory.Create(dateTimeAdder, addFromStart: true);
                    // Build DateTimes[]
                    // Get Tracker
                    // Track DateTimes[] to get positions[]
                    // Build LocalVector[] using the DateTimes and positions[]

                    // Force the client to resubmit the form.
                    _periodBuilder.Current.IsReady = false;
                    _periodBuilder.Save();
                }
            }

            TrackPeriodViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                Algorithms = Utilities.GetAlgorithmItems(),
                TimeUnits = Utilities.GetTimeUnitItems(),

                TrackForm = new TrackPeriodModel()
                {
                    Algorithm = values.Algorithm,
                    Start = Utilities.GetClientDateTime(_geoBuilder.CurrentGeolocation, values.Start),
                    IsFixed = values.IsFixed,
                    TimeUnit = values.TimeUnit,
                    Period = values.Period,
                    Interval = values.Interval
                },

                //Results = results,
                WarningMessage = message
            };

            return viewModel;
        }


        [HttpPost]
        public IActionResult SubmitPeriod(TrackPeriodModel trackForm, TrackPeriodRoute values)
        {
            // Check server-side validation state.
            if (!ModelState.IsValid)
            {
                // Re-render the view so that server-side validation messages are displayed.
                return View("Period", GetPeriodViewModel(values, buildResults: false));
            }

            // The ordering is important here.
            values.SetAlgorithm(trackForm.Algorithm);
            values.SetStart(trackForm.GetTicks());
            values.SetTimeUnit(trackForm.TimeUnit);
            values.SetFixed(trackForm.IsFixed);
            values.SetPeriod(trackForm.Period);
            values.SetInterval(trackForm.Interval);

            // Mark the entry as ready so we can calculate the results.
            trackForm.IsReady = true;

            // Set the current period entry.
            _periodBuilder.Current = trackForm;

            // Save the period entry to session state.
            _periodBuilder.Save();

            // Redirect the action to calculate the results.
            return RedirectToAction("Period", values.ToDictionary());
        }


        public ViewResult Period(TrackPeriodRoute values)
        {
            return View(GetPeriodViewModel(values, buildResults: true));
        }

        #endregion
    }
}

﻿using DST.Core.Observer;
using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.DataLayer.Repositories;
using DST.Models.DomainModels;
using DST.Models.Extensions;
using DST.Models.Routes;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DST.Models.DataLayer;
using DST.Core.Trajectory;
using DST.Core.TimeKeeper;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            SeasonModel season = _data.GetSeason(dso);
            ConstellationModel constellation = _data.GetConstellation(dso);
            Algorithm algorithm = Utilities.GetAlgorithm(values.Algorithm);

            TrackSummaryViewModel viewModel = new()
            {
                Dso = dso,
                CurrentRoute = values,
                DisplayInfo = Utilities.GetSummaryInfo(dso, season, constellation, _geoBuilder.CurrentGeolocation, algorithm),
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
            ILocalObserver localObserver = Utilities.GetLocalObserver(dso, _geoBuilder.CurrentGeolocation, algorithm);
            ITrajectory trajectory = TrajectoryCalculator.Calculate(localObserver);

            IEnumerable<TrackResult> results = Enumerable.Empty<TrackResult>();
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
                    results = Utilities.GetPhaseResults(localObserver, _phaseBuilder.Current);

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
                    IsTrackOnce = values.IsTrackOnce,
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
            values.SetTrackOnce(trackForm.IsTrackOnce);
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

            IEnumerable<TrackResult> results = Enumerable.Empty<TrackResult>();
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
                    results = Utilities.GetPeriodResults(localObserver, algorithm, _periodBuilder.Current);

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
            values.SetTrackOnce(trackForm.IsTrackOnce);
            values.SetTimeUnit(trackForm.TimeUnit);
            values.SetFixed(trackForm.IsFixed);
            values.SetAggregate(trackForm.IsAggregated);
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

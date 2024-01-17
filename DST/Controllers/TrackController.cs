﻿using DST.Core.Observer;
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

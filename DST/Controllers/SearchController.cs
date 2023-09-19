using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using DST.Models.DomainModels;
using DST.Models.DataLayer;
using DST.Models.DataLayer.Repositories;
using DST.Models.DataLayer.Query;
using DST.Models.ViewModels;
using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.Routes;

namespace DST.Controllers
{
    public class SearchController : Controller
    {
        #region Fields

        private readonly SearchUnitOfWork _data;
        private readonly ISearchBuilder _builder;
        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public SearchController(MainDbContext context, ISearchBuilder searchBuilder, IGeolocationBuilder geoBuilder)
        {
            _data = new SearchUnitOfWork(context);
            _builder = searchBuilder;

            // Load the client geolocation, if any.
            _geoBuilder = geoBuilder;
            _geoBuilder.Load();
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            // Load a saved route from session state, if any.
            _builder.Load();

            return RedirectToAction("List", _builder.Route.ToDictionary());
        }

        [HttpPost]
        public IActionResult CreateGeolocation(GeolocationModel geolocation, SearchRoute values, bool reset = false)
        {
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

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpGet]
        public IActionResult Clear(SearchRoute values)
        {
            // Clear the filters, but retain the paging and sorting values.
            values = values.Reset();

            return RedirectToAction("List", values.ToDictionary());
        }

        public ViewResult List(SearchRoute values)
        {
            // Save the current route to session state.
            _builder.Route = values;
            _builder.Save();

            // Set initial options from the route segments.
            SearchQueryOptions options = new()
            {
                // Include any initial navigation properties.
                //IncludeAll = "",

                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                SortDirection = values.SortDirection
            };

            options.SortFilter(values, _geoBuilder.CurrentGeolocation);

            SearchListViewModel viewModel = new()
            {
                Geolocation = _geoBuilder.CurrentGeolocation,

                TimeZoneItems = Utilities.GetTimeZoneItems(),

                DsoItems = _data.DsoItems.List(options),

                Types = _data.DsoTypes.List(new QueryOptions<DsoTypeModel>
                {
                    OrderBy = obj => obj.Type
                }),
                Catalogs = _data.Catalogs.List(new QueryOptions<CatalogModel>
                {
                    OrderBy = obj => obj.Name
                }),
                Constellations = _data.Constellations.List(new QueryOptions<ConstellationModel>
                {
                    OrderBy = obj => obj.Name
                }),
                Seasons = _data.Seasons.List(new QueryOptions<SeasonModel>
                {
                    OrderBy = obj => obj.Id
                }),

                Trajectories = Utilities.GetTrajectoryNames(),

                GetSortTag = Utilities.GetInfoFunc(values.SortField),

                CurrentRoute = values,
                TotalPages = values.GetTotalPages(_data.DsoItems.Count)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(string catalog, int id)
        {
            DsoModel dso = _data.DsoItems.Get(catalog, id);
            
            return View(dso);
        }

        #endregion
    }
}

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
using DST.Models.Extensions;

namespace DST.Controllers
{
    public class SearchController : Controller
    {
        #region Fields

        private readonly SearchUnitOfWork _data;
        private readonly ISearchRouteBuilder _routeBuilder;
        private readonly IGeolocationBuilder _geoBuilder;
        private readonly ISearchBuilder _searchBuilder;

        #endregion

        #region Constructors

        public SearchController(MainDbContext context, ISearchRouteBuilder routeBuilder, IGeolocationBuilder geoBuilder, ISearchBuilder searchBuilder)
        {
            _data = new SearchUnitOfWork(context);
            _routeBuilder = routeBuilder;
            _geoBuilder = geoBuilder;
            _searchBuilder = searchBuilder;

            // Load the client geolocation, if any.
            _geoBuilder.Load();

            // Load the client's previous search entry, if any.
            _searchBuilder.Load();
        }

        #endregion

        #region Methods

        private void ClearSearch()
        {
            // Only clear the search session if the stored input is not already empty.
            if (!_searchBuilder.CurrentSearch.IsEmpty)
            {
                // Clear the current SearchModel object.
                _searchBuilder.CurrentSearch.Clear();

                // Save the search entry in session.
                _searchBuilder.Save();
            }
        }

        public IActionResult Index()
        {
            // Load a saved route from session state, if any.
            _routeBuilder.Load();

            return RedirectToAction("List", _routeBuilder.Route.ToDictionary());
        }

        [HttpPost]
        public IActionResult SubmitGeolocation(GeolocationModel geolocation, SearchRoute values, bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("List", values.ToDictionary());
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

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpGet]
        public IActionResult ClearFilter(SearchRoute values, string name)
        {
            // Clear the filter route segment by name.
            if (values.TryClearFilter(name))
            {
                // Check if the Search route segment was cleared.
                if (name.EqualsExact(nameof(SearchRoute.Search)))
                {
                    // Clear the search session.
                    ClearSearch();
                }
            }

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpGet]
        public IActionResult ClearFilters(SearchRoute values)
        {
            // Clear all filters, but retain the paging and sorting values.
            values = values.Reset();

            // Clear the search session.
            ClearSearch();

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult PageSize(SearchRoute values, int size)
        {
            values.SetPageSize(size);

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult SubmitSearch(SearchModel search, SearchRoute values)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("List", values.ToDictionary());
            }

            // Set the route segment.
            values.SetSearch(search.Input);

            // Set the current SearchModel object.
            _searchBuilder.CurrentSearch = search;

            // Save the search entry in session.
            _searchBuilder.Save();

            return RedirectToAction("List", values.ToDictionary());
        }

        public ViewResult List(SearchRoute values)
        {
            // Validate route values.
            values.Validate();

            // Set initial options from the route segments.
            SearchQueryOptions options = new()
            {
                // Include any initial navigation properties.
                //IncludeAll = "",

                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                SortDirection = values.SortDirection
            };

            options.SortFilter(values, _geoBuilder.CurrentGeolocation, _searchBuilder.CurrentSearch);

            SearchListViewModel viewModel = new()
            {
                Geolocation = _geoBuilder.CurrentGeolocation,
                TimeZoneItems = Utilities.GetTimeZoneItems(),

                Search = _searchBuilder.CurrentSearch,

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
                PageSizes = Utilities.GetSearchListPageSizeItems()
            };

            int count = _data.DsoItems.Count;

            // The requested paging values are subject to user error when attempting to modify the URL directly.
            // Calculate the page number again, now that we know the total number of items after filtering.
            values.PageNumber = Paging.ClampPageNumber(count, values.PageSize, values.PageNumber);
            viewModel.TotalPages = values.GetTotalPages(count);
            viewModel.Results = values.GetResults(count);
            viewModel.CurrentRoute = values;

            // Save the current route to session state.
            _routeBuilder.Route = values;
            _routeBuilder.Save();

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

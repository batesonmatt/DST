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
        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public SearchController(MainDbContext context, IGeolocationBuilder geoBuilder)
        {
            _data = new SearchUnitOfWork(context);
            _geoBuilder = geoBuilder;
            _geoBuilder.Load();
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            return RedirectToAction("List");
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

        public ViewResult List(SearchRoute values)
        {
            if (values.ClearFilters)
            {
                // Clear the filters, but retain the paging and sorting values.
                values = values.Reset();
            }

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

        // Argument filterIds contains the posted values for ListFilter.Id.
        // Argument filters contains the posted values for ListFilter.Value in the order defined by filterIds.
        // Argument options contains the posted values for ToggleFilter.Id.
        //[HttpPost]
        //public RedirectToActionResult Filter(string[] filterIds, string[] filters, string[] options, bool clear = false)
        //{
        //    SearchRouteBuilder builder = new(HttpContext.Session);

        //    if (clear)
        //    {
        //        builder.ClearFilterSegments();
        //    }
        //    else
        //    {
        //        List<IFilter> form = new();

        //        if (filters?.Length <= filterIds?.Length)
        //        {
        //            for (int i = 0; i < filters.Length; i++)
        //            {
        //                form.Add(new ListFilter(filterIds[i], filters[i]));
        //            }
        //        }

        //        if (options?.Length > 0)
        //        {
        //            // The length of the array is variable since the input elements are posted only if they are checked.
        //            foreach (string id in options)
        //            {
        //                if (id is not null)
        //                {
        //                    form.Add(new ToggleFilter(id, ToggleFilter.On));
        //                }
        //            }
        //        }

        //        builder.LoadFilterSegments(form.ToArray());
        //    }

        //    builder.SaveRouteSegments();

        //    return RedirectToAction("List", builder.CurrentRoute);
        //}

        [HttpGet]
        public IActionResult Details(string catalog, int id)
        {
            DsoModel dso = _data.DsoItems.Get(catalog, id);
            
            return View(dso);
        }

        #endregion
    }
}

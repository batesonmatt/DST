using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using DST.Models.DomainModels;
using DST.Models.DataLayer;
using DST.Models.DataLayer.Repositories;
using DST.Models.DTOs;
using DST.Models.Grid;
using DST.Models.DataLayer.Query;
using DST.Models.ViewModels;

namespace DST.Controllers
{
    public class SearchController : Controller
    {
        #region Fields

        private readonly SearchUnitOfWork _data;
        private GeoLocationModel _geoLocation;

        #endregion

        #region Constructors

        public SearchController(MainDbContext context)
        {
            _data = new SearchUnitOfWork(context);
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            //ViewBag.LAT = 0.0;
            //ViewBag.LON = 0.0;

            return RedirectToAction("List");
        }

        public ViewResult List(SearchGridDTO values)
        {
            // Get GridBuilder object, load route segments, and store in session.
            SearchGridBuilder builder = new SearchGridBuilder(HttpContext.Session, values);

            SearchQueryOptions options = new SearchQueryOptions
            {
                // Include = "Constellation",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                SortDirection = builder.CurrentRoute.SortDirection
            };

            options.SortFilter(builder, _geoLocation);

            SearchListViewModel viewModel = new SearchListViewModel
            {
                GeoLocation = _geoLocation,

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

                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(_data.DsoItems.Count)
            };

            return View(viewModel);
        }

        /* Filter()
            
           if (model?.GeoLocation != null)
           {
               ViewBag.LAT = model.GeoLocation.Latitude;
               ViewBag.LON = model.GeoLocation.Longitude;
               _geoLocation = model.GeoLocation;
           }
         */

        // Argument filterIds contains the posted values for ListFilter.Id.
        // Argument filters contains the posted values for ListFilter.Value in the order defined by filterIds.
        // Argument options contains the posted values for ToggleFilter.Id.
        [HttpPost]
        public RedirectToActionResult Filter(string[] filterIds, string[] filters, string[] options, bool clear = false)
        {
            SearchGridBuilder builder = new SearchGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                List<IFilter> form = new List<IFilter>();

                if (filters?.Length <= filterIds?.Length)
                {
                    for (int i = 0; i < filters.Length; i++)
                    {
                        form.Add(new ListFilter(filterIds[i], filters[i]));
                    }
                }

                if (options?.Length > 0)
                {
                    // The length of the array is variable since the input elements are posted only if they are checked.
                    foreach (string id in options)
                    {
                        if (id != null)
                        {
                            form.Add(new ToggleFilter(id, ToggleFilter.On));
                        }
                    }
                }

                builder.LoadFilterSegments(form.ToArray());
            }

            builder.SaveRouteSegments();

            return RedirectToAction("List", builder.CurrentRoute);
        }

        [HttpGet]
        public IActionResult Details(string cat, int id)
        {
            DsoModel dso = _data.DsoItems.Get(cat, id);
            
            return View(dso);
        }

        #endregion
    }
}

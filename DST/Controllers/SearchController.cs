using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using DST.Models.DomainModels;
using DST.Models.DataLayer;
using DST.Models.DataLayer.Repositories;
using DST.Models.DTOs;
using DST.Models.Builders.Routing;
using DST.Models.DataLayer.Query;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using DST.Models.Extensions;
using System;
using System.Linq;
using DST.Models.Builders;

namespace DST.Controllers
{
    public class SearchController : Controller
    {
        #region Fields

        private readonly SearchUnitOfWork _data;

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
            return RedirectToAction("List");
        }

        public IActionResult CreateGeolocation(GeolocationModel geolocation, SearchDTO values, bool reset = false)
        {
            // Replace nulls with an empty string.
            geolocation.TimeZoneId ??= string.Empty;
            geolocation.UserTimeZoneId ??= string.Empty;

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                geolocation.Reset();
            }
            else if (geolocation.TimeZoneId != string.Empty)
            {
                // Verify the selected id.
                geolocation.VerifyAndUpdateTimeZone(geolocation.TimeZoneId);
            }
            else if (geolocation.UserTimeZoneId != string.Empty)
            {
                // Try to verify the retrieved IANA id.
                geolocation.VerifyAndUpdateTimeZone(geolocation.UserTimeZoneId);
            }
            else
            {
                // No timezone was selected/found. Default to UTC.
                geolocation.ResetTimeZone();
            }

            // Store the GeolocationModel object in session.
            GeolocationBuilder geoBuilder = new(HttpContext.Session, geolocation);
            geoBuilder.SaveGeolocation();

            return RedirectToAction("List", values);
        }

        public ViewResult List(SearchDTO values)
        {
            // Get a new the GeolocationBuilder for the current session.
            GeolocationBuilder geoBuilder = new(HttpContext.Session);

            // Get a new GridBuilder object, load route segments, and store in the current session.
            SearchRouteBuilder builder = new(HttpContext.Session, values);

            // if clear filters:
            // builder.ClearFilterSegments();
            // else:
            builder.SaveRouteSegments();

            SearchQueryOptions options = new()
            {
                // Include = "Constellation",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                SortDirection = builder.CurrentRoute.SortDirection
            };
            
            options.SortFilter(builder, geoBuilder);

            SearchListViewModel viewModel = new()
            {
                Geolocation = geoBuilder.CurrentGeolocation,

                TimeZoneItems = TimeZoneInfo.GetSystemTimeZones()
                    .OrderByDescending(t => t.Id == GeolocationModel.DefaultId)
                    .ThenBy(t => t.BaseUtcOffset.TotalHours)
                    .Select(t => new TimeZoneItem(t.Id, t.DisplayName)),

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
            
           if (model?.GeoLocation is not null)
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
            SearchRouteBuilder builder = new(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                List<IFilter> form = new();

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
                        if (id is not null)
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

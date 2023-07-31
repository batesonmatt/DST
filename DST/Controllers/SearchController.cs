﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using DST.Models.DomainModels;
using DST.Models.DataLayer;
using DST.Models.DataLayer.Repositories;
using DST.Models.DTOs;
using DST.Models.Grid;
using DST.Models.DataLayer.Query;
using DST.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using DST.Models.Extensions;

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

        public IActionResult Test(GeolocationModel geolocation)
        {
            HttpContext.Session.SetString("TZ", geolocation.TimeZoneName);
            HttpContext.Session.SetObject("LAT", geolocation.Latitude);
            HttpContext.Session.SetObject("LON", geolocation.Longitude);

            return RedirectToAction("List");
        }

        public ViewResult List(SearchGridDTO values)
        {
            GeolocationModel geolocation = new()
            {
                TimeZoneName = HttpContext.Session.GetString("TZ") ?? string.Empty,
                Latitude = HttpContext.Session.GetObject<double>("LAT"),
                Longitude = HttpContext.Session.GetObject<double>("LON")
            };

            // Get GridBuilder object, load route segments, and store in session.
            SearchGridBuilder builder = new(HttpContext.Session, values);

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
            
            options.SortFilter(builder, geolocation);

            SearchListViewModel viewModel = new()
            {
                Geolocation = geolocation,

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
            SearchGridBuilder builder = new(HttpContext.Session);

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

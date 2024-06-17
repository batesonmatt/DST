using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using DST.Models.DomainModels;
using DST.Models.DataLayer.Repositories;
using DST.Models.DataLayer.Query;
using DST.Models.ViewModels;
using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.Routes;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;

namespace DST.Controllers
{
    public class SearchController : Controller
    {
        #region Methods

        public IActionResult Index([FromServices] ISearchRouteBuilder routeBuilder)
        {
            // Load a saved route from session state, if any.
            routeBuilder.Load();

            return RedirectToAction("List", routeBuilder.Route.ToDictionary());
        }

        [HttpPost]
        public IActionResult SubmitGeolocation(
            [FromServices] IGeolocationBuilder geoBuilder,
            GeolocationModel geolocation,
            SearchRoute values,
            bool reset = false)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("List", values.ToDictionary());
            }

            // Load the client geolocation, if any.
            geoBuilder.Load();

            if (reset)
            {
                // Reset geolocation and timezone to defaults.
                geoBuilder.CurrentGeolocation.Reset();
            }
            else
            {
                // Update geolocation and timezone.
                geoBuilder.CurrentGeolocation.SetGeolocation(geolocation);
            }

            // Save the geolocation in session and create a persistent cookie.
            geoBuilder.Save();

            return RedirectToAction("List", values.ToDictionary());
        }

        private static void ClearSearch(ISearchBuilder searchBuilder)
        {
            // Load the client's previous search entry, if any.
            searchBuilder.Load();

            // Only clear the search session if the stored input is not already empty.
            if (!searchBuilder.CurrentSearch.IsEmpty)
            {
                // Clear the current SearchModel object.
                searchBuilder.CurrentSearch.Clear();

                // Save the search entry in session.
                searchBuilder.Save();
            }
        }

        [HttpGet]
        public IActionResult ClearFilter(
            [FromServices] ISearchBuilder searchBuilder,
            SearchRoute values,
            string name)
        {
            // Clear the filter route segment by name.
            if (values.TryClearFilter(name))
            {
                // Check if the Search route segment was cleared.
                if (name.EqualsExact(nameof(SearchRoute.Search)))
                {
                    // Clear the search session.
                    ClearSearch(searchBuilder);
                }
            }

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpGet]
        public IActionResult ClearFilters(
            [FromServices] ISearchBuilder searchBuilder,
            SearchRoute values)
        {
            // Clear all filters, but retain the paging and sorting values.
            values = values.Reset();

            // Clear the search session.
            ClearSearch(searchBuilder);

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult PageSize(SearchRoute values, int size)
        {
            values.SetPageSize(size);

            return RedirectToAction("List", values.ToDictionary());
        }

        [HttpPost]
        public IActionResult SubmitSearch(
            [FromServices] ISearchBuilder searchBuilder,
            SearchModel search,
            SearchRoute values)
        {
            // Check model state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("List", values.ToDictionary());
            }

            // Set the route segment.
            values.SetSearch(search.Input);

            // Load the client's previous search entry, if any.
            searchBuilder.Load();

            // Set the current SearchModel object.
            searchBuilder.CurrentSearch = search;

            // Save the search entry in session.
            searchBuilder.Save();

            return RedirectToAction("List", values.ToDictionary());
        }

        public ViewResult List(
            [FromServices] ISearchUnitOfWork data,
            [FromServices] ISearchRouteBuilder routeBuilder,
            [FromServices] IGeolocationBuilder geoBuilder,
            [FromServices] ISearchBuilder searchBuilder,
            SearchRoute values)
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

            // Load the client geolocation, if any.
            geoBuilder.Load();

            // Load the client's previous search entry, if any.
            searchBuilder.Load();

            options.SortFilter(values, geoBuilder.CurrentGeolocation, searchBuilder.CurrentSearch);

            IEnumerable<SelectListItem> pageSizes = Enumerable.Range(1, 5).Select(
                    i => new SelectListItem(
                        string.Format(Resources.DisplayText.PageSizeFormat, i * 10),
                        (i * 10).ToString(),
                        (i * 10) == values.PageSize));

            SearchListViewModel viewModel = new()
            {
                Search = searchBuilder.CurrentSearch,

                DsoItems = data.DsoItems.List(options),

                Types = data.DsoTypes.List(new QueryOptions<DsoTypeModel>
                {
                    OrderBy = obj => obj.Type
                }),
                Catalogs = data.Catalogs.List(new QueryOptions<CatalogModel>
                {
                    OrderBy = obj => obj.Name
                }),
                Constellations = data.Constellations.List(new QueryOptions<ConstellationModel>
                {
                    OrderBy = obj => obj.Name
                }),
                Seasons = data.Seasons.List(new QueryOptions<SeasonModel>
                {
                    OrderBy = obj => obj.Id
                }),

                Trajectories = Utilities.GetTrajectoryNames(),
                PageSizes = pageSizes
            };

            int count = data.DsoItems.Count;

            // The requested paging values are subject to user error when attempting to modify the URL directly.
            // Calculate the page number again, now that we know the total number of items after filtering.
            values.PageNumber = Paging.ClampPageNumber(count, values.PageSize, values.PageNumber);
            viewModel.TotalPages = values.GetTotalPages(count);
            viewModel.Results = values.GetResults(count);
            viewModel.CurrentRoute = values;

            // Save the current route to session state.
            routeBuilder.Route = values;
            routeBuilder.Save();

            return View(viewModel);
        }

        #endregion
    }
}

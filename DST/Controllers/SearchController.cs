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
        
        [HttpPost]
        public IActionResult SubmitSortFilter(
            //[FromServices] ISortFilterBuilder sortFilterBuilder,
            SortFilterModel sortFilter,
            SearchRoute values,
            bool reset = false)
        {
            // Check server-side validation state.
            if (!ModelState.IsValid)
            {
                // The view is not being re-rendered here, so any server-side validation messages will not be shown.
                return RedirectToAction("List", values.ToDictionary());
            }

            // Load the previous sort-filter entry, if any.
            // sortFilterBuilder.Load();

            if (reset)
            {
                // Clear all filters, paging, and sorting values.
                // Retain the search data and session state.
                values = values.ResetOptions();
            }
            else
            {
                // Set the current sort-filter entry.
                // sortFilterBuilder.CurrentSortFilter = sortFilter;

                values.SetSort(sortFilter.SortField);
                values.SetSortDirection(sortFilter.SortDirection);
                values.SetPageSize(sortFilter.PageSize);
                values.SetCatalog(sortFilter.Catalog);
                values.SetType(sortFilter.Type);
                values.SetConstellation(sortFilter.Constellation);
                values.SetSeason(sortFilter.Season);
                values.SetTrajectory(sortFilter.Trajectory);
                values.SetVisibility(sortFilter.Visibility);
                values.SetHasName(sortFilter.IsFilterByHasName);
            }

            // Save the sort-filter entry to session state.
            // sortFilterBuilder.Save();

            return RedirectToAction("List", values.ToDictionary());
        }

        public ViewResult List(
            [FromServices] ISearchUnitOfWork data,
            [FromServices] ISearchRouteBuilder routeBuilder,
            [FromServices] IGeolocationBuilder geoBuilder,
            [FromServices] ISearchBuilder searchBuilder,
            //[FromServices] ISortFilterBuilder sortFilterBuilder,
            SearchRoute values)
        {
            // Validate route values.
            values.Validate();

            // Load the client geolocation, if any.
            geoBuilder.Load();

            // Load the client's previous search entry, if any.
            searchBuilder.Load();

            // Load the previous sort-filter entry, if any.
            // sortFilterBuilder.Load();

            // Set initial options from the route segments.
            SearchQueryOptions options = new()
            {
                // Include any initial navigation properties.
                //IncludeAll = "",

                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
                SortDirection = values.SortDirection
            };

            options.SortFilter(values, geoBuilder.CurrentGeolocation, searchBuilder.CurrentSearch);

            IEnumerable<SelectListItem> sortFields = Sort.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.SortField));

            IEnumerable<SelectListItem> pageSizes = Enumerable.Range(1, 5).Select(
                i => new SelectListItem(
                    string.Format(Resources.DisplayText.PageSizeFormat, i * 10),
                    (i * 10).ToString(),
                    (i * 10) == values.PageSize));

            IEnumerable<SelectListItem> catalogs = data.GetCatalogTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Catalog));

            IEnumerable<SelectListItem> types = data.GetTypeTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Type));

            IEnumerable<SelectListItem> constellations = data.GetConstellationTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Constellation));

            IEnumerable<SelectListItem> seasons = data.GetSeasonTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Season));

            IEnumerable<SelectListItem> trajectories = TrajectoryName.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Trajectory));

            IEnumerable<SelectListItem> visibilities = VisibilityName.GetTextValuePairs().Select(
                i => new SelectListItem(i.Text, i.Value, i.Value == values.Visibility));

            SearchListViewModel viewModel = new()
            {
                Search = searchBuilder.CurrentSearch,

                SortFilter = new SortFilterModel()
                {
                    SortField = values.SortField,
                    SortDirection = values.SortDirection,
                    PageSize = values.PageSize,
                    Catalog = values.Catalog,
                    Type = values.Type,
                    Constellation = values.Constellation,
                    Season = values.Season,
                    Trajectory = values.Trajectory,
                    Visibility = values.Visibility,
                    IsFilterByHasName = values.IsFilterByHasName
                },

                SortFields = sortFields,
                PageSizes = pageSizes,
                DsoItems = data.DsoItems.List(options),
                Catalogs = catalogs,
                Types = types,
                Constellations = constellations,
                Seasons = seasons,
                Trajectories = trajectories,
                Visibilities = visibilities
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

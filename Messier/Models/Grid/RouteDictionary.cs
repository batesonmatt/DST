using Messier.Models.DataLayer.Query;
using Messier.Models.DTOs;
using Messier.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Messier.Models.Grid
{
    public class RouteDictionary : Dictionary<string, string>
    {
        #region Properties

        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public ListFilter TypeFilter
        {
            get => new ListFilter(FilterKeys[0], Get(FilterKeys[0]));
            set => this[FilterKeys[0]] = value.Value;
        }

        public ListFilter CatalogFilter
        {
            get => new ListFilter(FilterKeys[1], Get(FilterKeys[1]));
            set => this[FilterKeys[1]] = value.Value;
        }

        public ListFilter ConstellationFilter
        {
            get => new ListFilter(FilterKeys[2], Get(FilterKeys[2]));
            set => this[FilterKeys[2]] = value.Value;
        }

        public ListFilter SeasonFilter
        {
            get => new ListFilter(FilterKeys[3], Get(FilterKeys[3]));
            set => this[FilterKeys[3]] = value.Value;
        }

        public ToggleFilter LocalFilter
        {
            get => new ToggleFilter(FilterKeys[4], Get(FilterKeys[4]));
            set => this[FilterKeys[4]] = value.Value;
        }

        public ToggleFilter HasNameFilter
        {
            get => new ToggleFilter(FilterKeys[5], Get(FilterKeys[5]));
            set => this[FilterKeys[5]] = value.Value;
        }

        public ToggleFilter VisibilityFilter
        {
            get => new ToggleFilter(FilterKeys[6], Get(FilterKeys[6]));
            set => this[FilterKeys[6]] = value.Value;
        }

        public ToggleFilter RiseTimeFilter
        {
            get => new ToggleFilter(FilterKeys[7], Get(FilterKeys[7]));
            set => this[FilterKeys[7]] = value.Value;
        }

        public ListFilter TrajectoryFilter
        {
            get => new ListFilter(FilterKeys[8], Get(FilterKeys[8]));
            set => this[FilterKeys[8]] = value.Value;
        }

        private static string[] FilterKeys
            => new string[]
            {
                nameof(SearchGridDTO.Type),
                nameof(SearchGridDTO.Catalog),
                nameof(SearchGridDTO.Constellation),
                nameof(SearchGridDTO.Season),
                nameof(SearchGridDTO.Local),
                nameof(SearchGridDTO.HasName),
                nameof(SearchGridDTO.Visibility),
                nameof(SearchGridDTO.RiseTime),
                nameof(SearchGridDTO.Trajectory)
            };

        private IFilter[] Filters
            => new IFilter[]
            {
                TypeFilter,
                CatalogFilter,
                ConstellationFilter,
                SeasonFilter,
                LocalFilter,
                HasNameFilter,
                VisibilityFilter,
                RiseTimeFilter,
                TrajectoryFilter
            };

        #endregion

        #region Methods

        private string Get(string key)
        {
            if (Keys == null || key == null)
            {
                return null;
            }

            return Keys.Contains(key) ? this[key] : null;
        }

        private void Set(string key, string value)
        {
            if (Keys != null && key != null)
            {
                if (Keys.Contains(key))
                {
                    this[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public void SetSort(string fieldName)
            => SortField = string.IsNullOrWhiteSpace(fieldName) ? Sort.Default : fieldName;

        public void SetDirection(string direction)
            => SortDirection = string.IsNullOrWhiteSpace(direction) ? OrderDirection.Default : direction;

        // Sets the sorting field for a table and toggles the sorting direction on subsequent calls.
        public void SetTableSort(string fieldName, RouteDictionary current)
        {
            SortField = fieldName;

            if (current.SortField.EqualsIgnoreCase(fieldName) && current.SortDirection == OrderDirection.Ascending)
            {
                SortDirection = OrderDirection.Descending;
            }
            else
            {
                // Always start in ascending order.
                SortDirection = OrderDirection.Ascending;
            }
        }

        public RouteDictionary Clone()
        {
            RouteDictionary clone = new RouteDictionary();

            if (Keys != null)
            {
                foreach (string key in Keys)
                {
                    clone.Add(key, this[key]);
                }
            }

            return clone;
        }

        public void ClearFilters()
        {
            if (Keys != null)
            {
                string[] keys = FilterKeys;
                IFilter[] filters = Filters;

                for (int i = 0; i < keys.Length; i++)
                {
                    filters[i].Reset();

                    Set(keys[i], filters[i].Value);
                }
            }
        }

        public void SetFilters(IFilter[] filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            foreach (IFilter filter in filters)
            {
                Set(filter.Id, filter.Value);
            }

            // Insert default values for any filter key that was not specified.
            /* This should only be called once during this instance's lifespan */
            for (int i = 0; i < FilterKeys.Length; i++)
            {
                if (ContainsKey(FilterKeys[i]) == false)
                {
                    Set(FilterKeys[i], Filters[i].Value);
                }
            }
        }

        #endregion
    }
}

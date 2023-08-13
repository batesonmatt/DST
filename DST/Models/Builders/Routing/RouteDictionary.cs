﻿using DST.Models.DataLayer.Query;
using DST.Models.DTOs;
using DST.Models.Extensions;
using System;
using System.Collections.Generic;

namespace DST.Models.Builders.Routing
{
    public class RouteDictionary : Dictionary<string, string>
    {
        #region Properties

        public int PageNumber
        {
            get => Get(nameof(PageSortDTO.PageNumber)).ToInt();
            set => this[nameof(PageSortDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(PageSortDTO.PageSize)).ToInt();
            set => this[nameof(PageSortDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(PageSortDTO.SortField));
            set => this[nameof(PageSortDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(PageSortDTO.SortDirection));
            set => this[nameof(PageSortDTO.SortDirection)] = value;
        }

        public ListFilter TypeFilter
        {
            get => new(FilterKeys[0], Get(FilterKeys[0]));
            set => this[FilterKeys[0]] = value.Value;
        }

        public ListFilter CatalogFilter
        {
            get => new(FilterKeys[1], Get(FilterKeys[1]));
            set => this[FilterKeys[1]] = value.Value;
        }

        public ListFilter ConstellationFilter
        {
            get => new(FilterKeys[2], Get(FilterKeys[2]));
            set => this[FilterKeys[2]] = value.Value;
        }

        public ListFilter SeasonFilter
        {
            get => new(FilterKeys[3], Get(FilterKeys[3]));
            set => this[FilterKeys[3]] = value.Value;
        }

        public ListFilter TrajectoryFilter
        {
            get => new(FilterKeys[4], Get(FilterKeys[4]));
            set => this[FilterKeys[4]] = value.Value;
        }

        public ToggleFilter LocalFilter
        {
            get => new(FilterKeys[5], Get(FilterKeys[5]));
            set => this[FilterKeys[5]] = value.Value;
        }

        public ToggleFilter VisibleFilter
        {
            get => new(FilterKeys[6], Get(FilterKeys[6]));
            set => this[FilterKeys[6]] = value.Value;
        }

        public ToggleFilter RisingFilter
        {
            get => new(FilterKeys[7], Get(FilterKeys[7]));
            set => this[FilterKeys[7]] = value.Value;
        }

        public ToggleFilter HasNameFilter
        {
            get => new(FilterKeys[8], Get(FilterKeys[8]));
            set => this[FilterKeys[8]] = value.Value;
        }

        private static string[] FilterKeys
            => new string[]
            {
                nameof(SearchDTO.Type),
                nameof(SearchDTO.Catalog),
                nameof(SearchDTO.Constellation),
                nameof(SearchDTO.Season),
                nameof(SearchDTO.Trajectory),
                nameof(SearchDTO.Local),
                nameof(SearchDTO.Visible),
                nameof(SearchDTO.Rising),
                nameof(SearchDTO.HasName)
            };

        private IFilter[] Filters
            => new IFilter[]
            {
                TypeFilter,
                CatalogFilter,
                ConstellationFilter,
                SeasonFilter,
                TrajectoryFilter,
                LocalFilter,
                VisibleFilter,
                RisingFilter,
                HasNameFilter
            };

        #endregion

        #region Methods

        private string Get(string key)
        {
            if (Keys is null || key is null)
            {
                return string.Empty;
            }

            return TryGetValue(key, out string value) ? value : string.Empty;
        }

        private void Set(string key, string value)
        {
            if (Keys is not null && key is not null)
            {
                if (ContainsKey(key))
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
            RouteDictionary clone = new();

            if (Keys is not null)
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
            if (Keys is not null)
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
            _ = filters ?? throw new ArgumentNullException(nameof(filters));

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

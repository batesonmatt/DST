using DST.Models.DataLayer.Query;
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
            get => new(nameof(SearchDTO.Type), Get(nameof(SearchDTO.Type)));
            set => this[nameof(SearchDTO.Type)] = value.Value;
        }

        public ListFilter CatalogFilter
        {
            get => new(nameof(SearchDTO.Catalog), Get(nameof(SearchDTO.Catalog)));
            set => this[nameof(SearchDTO.Catalog)] = value.Value;
        }

        public ListFilter ConstellationFilter
        {
            get => new(nameof(SearchDTO.Constellation), Get(nameof(SearchDTO.Constellation)));
            set => this[nameof(SearchDTO.Constellation)] = value.Value;
        }

        public ListFilter SeasonFilter
        {
            get => new(nameof(SearchDTO.Season), Get(nameof(SearchDTO.Season)));
            set => this[nameof(SearchDTO.Season)] = value.Value;
        }

        public ListFilter TrajectoryFilter
        {
            get => new(nameof(SearchDTO.Trajectory), Get(nameof(SearchDTO.Trajectory)));
            set => this[nameof(SearchDTO.Trajectory)] = value.Value;
        }

        public ToggleFilter LocalFilter
        {
            get => new(nameof(SearchDTO.Local), Get(nameof(SearchDTO.Local)));
            set => this[nameof(SearchDTO.Local)] = value.Value;
        }

        public ToggleFilter VisibleFilter
        {
            get => new(nameof(SearchDTO.Visible), Get(nameof(SearchDTO.Visible)));
            set => this[nameof(SearchDTO.Visible)] = value.Value;
        }

        public ToggleFilter RisingFilter
        {
            get => new(nameof(SearchDTO.Rising), Get(nameof(SearchDTO.Rising)));
            set => this[nameof(SearchDTO.Rising)] = value.Value;
        }

        public ToggleFilter HasNameFilter
        {
            get => new(nameof(SearchDTO.HasName), Get(nameof(SearchDTO.HasName)));
            set => this[nameof(SearchDTO.HasName)] = value.Value;
        }

        private string[] FilterKeys
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

            string[] keys = FilterKeys;
            IFilter[] filterValues = Filters;

            // Insert default values for any filter key that was not specified.
            /* This should only be called once during this instance's lifespan */
            for (int i = 0; i < keys.Length; i++)
            {
                if (ContainsKey(keys[i]) == false)
                {
                    Set(keys[i], filterValues[i].Value);
                }
            }
        }

        #endregion
    }
}

using DST.Models.DataLayer.Query;
using DST.Models.DomainModels;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DST.Models.Routes
{
    public class SearchRoute : PageSortRoute, IRouteDictionary<SearchRoute>
    {
        #region Properties

        public string Type { get; set; } = Filter.All;
        public string Catalog { get; set; } = Filter.All;
        public string Constellation { get; set; } = Filter.All;
        public string Season { get; set; } = Filter.All;
        public string Trajectory { get; set; } = Filter.All;
        public string Local { get; set; } = Filter.Off;
        public string Visible { get; set; } = Filter.Off;
        public string Rising { get; set; } = Filter.Off;
        public string HasName { get; set; } = Filter.Off;
        public string Search {  get; set; } = string.Empty;

        [JsonIgnore] public bool IsSortById => SortField.EqualsSeo(Sort.Id);
        [JsonIgnore] public bool IsSortByName => SortField.EqualsSeo(Sort.Name);
        [JsonIgnore] public bool IsSortByType => SortField.EqualsSeo(Sort.Type);
        [JsonIgnore] public bool IsSortByConstellation => SortField.EqualsSeo(Sort.Constellation);
        [JsonIgnore] public bool IsSortByDistance => SortField.EqualsSeo(Sort.Distance);
        [JsonIgnore] public bool IsSortByBrightness => SortField.EqualsSeo(Sort.Brightness);
        [JsonIgnore] public bool IsSortByRiseTime => SortField.EqualsSeo(Sort.RiseTime);
        [JsonIgnore] public bool IsFilterByType => !Type.IsFilterAll();
        [JsonIgnore] public bool IsFilterByCatalog => !Catalog.IsFilterAll();
        [JsonIgnore] public bool IsFilterByConstellation => !Constellation.IsFilterAll();
        [JsonIgnore] public bool IsFilterBySeason => !Season.IsFilterAll();
        [JsonIgnore] public bool IsFilterByTrajectory => !Trajectory.IsFilterAll();
        [JsonIgnore] public bool IsFilterByLocal => Local.IsFilterOn();
        [JsonIgnore] public bool IsFilterByVisible => Visible.IsFilterOn();
        [JsonIgnore] public bool IsFilterByRising => Rising.IsFilterOn();
        [JsonIgnore] public bool IsFilterByHasName => HasName.IsFilterOn();
        [JsonIgnore] public bool HasSearch => !string.IsNullOrWhiteSpace(Search);

        #endregion

        #region Constructors

        public SearchRoute() { }

        public SearchRoute(PageSortRoute values) : base(values) { }

        #endregion

        #region Methods

        public bool HasFilters()
        {
            if (IsFilterByType) return true;
            if (IsFilterByCatalog) return true;
            if (IsFilterByConstellation) return true;
            if (IsFilterBySeason) return true;
            if (IsFilterByTrajectory) return true;
            if (IsFilterByLocal) return true;
            if (IsFilterByVisible) return true;
            if (IsFilterByRising) return true;
            if (IsFilterByHasName) return true;
            if (HasSearch) return true;

            return false;
        }

        public bool TryClearFilter(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            switch (name)
            {
                case nameof(Type) when IsFilterByType:
                    Type = Filter.All;
                    return true;
                case nameof(Catalog) when IsFilterByCatalog:
                    Catalog = Filter.All;
                    return true;
                case nameof(Constellation) when IsFilterByConstellation:
                    Constellation = Filter.All;
                    return true;
                case nameof(Season) when IsFilterBySeason:
                    Season = Filter.All;
                    return true;
                case nameof(Trajectory) when IsFilterByTrajectory:
                    Trajectory = Filter.All;
                    return true;
                case nameof(Local) when IsFilterByLocal:
                    Local = Filter.Off;
                    return true;
                case nameof(Visible) when IsFilterByVisible:
                    Visible = Filter.Off;
                    return true;
                case nameof(Rising) when IsFilterByRising:
                    Rising = Filter.Off;
                    return true;
                case nameof(HasName) when IsFilterByHasName:
                    HasName = Filter.Off;
                    return true;
                case nameof(Search) when HasSearch:
                    ClearSearch();
                    return true;
                default:
                    return false;
            }
        }

        public SearchRoute Reset()
        {
            return new SearchRoute(this as PageSortRoute);
        }

        public void SetType(string type)
        {
            Type = string.IsNullOrWhiteSpace(type) ? Filter.All : type.Trim();
        }

        public void SetCatalog(string catalog)
        {
            Catalog = string.IsNullOrWhiteSpace(catalog) ? Filter.All : catalog.Trim();
        }

        public void SetConstellation(string constellation)
        {
            Constellation = string.IsNullOrWhiteSpace(constellation) ? Filter.All : constellation.Trim();
        }

        public void SetSeason(string season)
        {
            Season = string.IsNullOrWhiteSpace(season) ? Filter.All : season.Trim();
        }

        public void SetTrajectory(string trajectory)
        {
            Trajectory = string.IsNullOrWhiteSpace(trajectory) ? Filter.All : trajectory.Trim();
        }

        public void SetSearch(string input)
        {
            Search = string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim();
        }

        public void ClearSearch()
        {
            Search = string.Empty;
        }

        public IDictionary<string, string> GetActiveFilters()
        {
            Dictionary<string, string> filters = new() { };

            if (IsFilterByType) filters.Add(nameof(Type), Resources.DisplayText.FilterType);
            if (IsFilterByCatalog) filters.Add(nameof(Catalog), Resources.DisplayText.FilterCatalog);
            if (IsFilterByConstellation) filters.Add(nameof(Constellation), Resources.DisplayText.FilterConstellation);
            if (IsFilterBySeason) filters.Add(nameof(Season), Resources.DisplayText.FilterSeason);
            if (IsFilterByTrajectory) filters.Add(nameof(Trajectory), Resources.DisplayText.FilterTrajectory);
            if (IsFilterByLocal) filters.Add(nameof(Local), Resources.DisplayText.FilterLocal);
            if (IsFilterByVisible) filters.Add(nameof(Visible), Resources.DisplayText.FilterVisible);
            if (IsFilterByRising) filters.Add(nameof(Rising), Resources.DisplayText.FilterRising);
            if (IsFilterByHasName) filters.Add(nameof(HasName), Resources.DisplayText.FilterName);
            if (HasSearch) filters.Add(nameof(Search), Resources.DisplayText.FilterSearch);

            return filters;
        }

        public new SearchRoute Clone()
        {
            return (SearchRoute)MemberwiseClone();
        }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> route = new()
            {
                { nameof(Type), Type.ToKebabCase() },
                { nameof(Catalog), Catalog.ToKebabCase() },
                { nameof(Constellation), Constellation.ToKebabCase() },
                { nameof(Season), Season.ToKebabCase() },
                { nameof(Trajectory), Trajectory.ToKebabCase() },
                { nameof(Local), Local.ToKebabCase() },
                { nameof(Visible), Visible.ToKebabCase() },
                { nameof(Rising), Rising.ToKebabCase() },
                { nameof(HasName), HasName.ToKebabCase() },
                { nameof(Search), Search.ToKebabCase() },
            };

            return base.ToDictionary()
                .Concat(route)
                .ToDictionary(segment => segment.Key, segment => segment.Value);
        }

        public override void Validate()
        {
            base.Validate();

            if (!(IsSortById || IsSortByName || IsSortByType || IsSortByConstellation || 
                IsSortByDistance || IsSortByBrightness || IsSortByRiseTime))
            {
                SortField = Sort.Default;
            }

            if (IsFilterByType)
            {
                SetType(Type);
            }
            if (IsFilterByCatalog)
            {
                SetCatalog(Catalog);
            }
            if (IsFilterByConstellation)
            {
                SetConstellation(Constellation);
            }
            if (IsFilterBySeason)
            {
                SetSeason(Season);
            }
            if (IsFilterByTrajectory)
            {
                SetTrajectory(Trajectory);
            }

            if (!(Local.IsFilterOn() || Local.IsFilterOff()))
            {
                Local = Filter.Off;
            }

            if (!(Visible.IsFilterOn() || Visible.IsFilterOff()))
            {
                Visible = Filter.Off;
            }

            if (!(Rising.IsFilterOn() || Rising.IsFilterOff()))
            {
                Rising = Filter.Off;
            }

            if (!(HasName.IsFilterOn() || HasName.IsFilterOff()))
            {
                HasName = Filter.Off;
            }

            if (HasSearch)
            {
                if (Search.Length > SearchModel.MaxInputLength)
                {
                    Search = Search[..SearchModel.MaxInputLength];
                }

                Search = Search.Trim();
            }
        }

        #endregion
    }
}
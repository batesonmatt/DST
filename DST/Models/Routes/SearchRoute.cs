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

        public SearchRoute Reset()
        {
            return new SearchRoute(this as PageSortRoute);
        }

        public void SetSearch(string input)
        {
            Search = string.IsNullOrWhiteSpace(input) ? string.Empty : input;
        }

        public void ClearSearch()
        {
            Search = string.Empty;
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
                Search = Search.Trim();

                if (Search.Length > SearchModel.MaxInputLength)
                {
                    Search = Search[..SearchModel.MaxInputLength];
                }
            }
        }

        #endregion
    }
}
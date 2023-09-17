using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DST.Models.Routes
{
    /* Use static class/const values for the default values, "All"=>"all", "Off"=>"off", "On"=>"on" */

    public class SearchRoute : PageSortRoute, IRouteDictionary<SearchRoute>
    {
        #region Properties

        public string Type { get; set; } = "all";
        public string Catalog { get; set; } = "all";
        public string Constellation { get; set; } = "all";
        public string Season { get; set; } = "all";
        public string Trajectory { get; set; } = "all";
        public string Local { get; set; } = "off";
        public string Visible { get; set; } = "off";
        public string Rising { get; set; } = "off";
        public string HasName { get; set; } = "off";

        [JsonIgnore] public bool IsSortById => SortField.EqualsSeo(Sort.Id);
        [JsonIgnore] public bool IsSortByName => SortField.EqualsSeo(Sort.Name);
        [JsonIgnore] public bool IsSortByType => SortField.EqualsSeo(Sort.Type);
        [JsonIgnore] public bool IsSortByConstellation => SortField.EqualsSeo(Sort.Constellation);
        [JsonIgnore] public bool IsSortByDistance => SortField.EqualsSeo(Sort.Distance);
        [JsonIgnore] public bool IsSortByBrightness => SortField.EqualsSeo(Sort.Brightness);
        [JsonIgnore] public bool IsSortByRiseTime => SortField.EqualsSeo(Sort.RiseTime);
        [JsonIgnore] public bool IsFilterByType => Type != "all";
        [JsonIgnore] public bool IsFilterByCatalog => Catalog != "all";
        [JsonIgnore] public bool IsFilterByConstellation => Constellation != "all";
        [JsonIgnore] public bool IsFilterBySeason => Season != "all";
        [JsonIgnore] public bool IsFilterByTrajectory => Trajectory != "all";
        [JsonIgnore] public bool IsFilterByLocal => Local != "off";
        [JsonIgnore] public bool IsFilterByVisible => Visible != "off";
        [JsonIgnore] public bool IsFilterByRising => Rising != "off";
        [JsonIgnore] public bool IsFilterByHasName => HasName != "off";

        #endregion

        #region Methods

        /* public void ClearFilters() => reset all filters to default values */

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
            };

            return base.ToDictionary()
                .Concat(route)
                .ToDictionary(segment => segment.Key, segment => segment.Value);
        }

        #endregion
    }
}
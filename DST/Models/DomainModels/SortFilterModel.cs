using DST.Models.DataLayer.Query;

namespace DST.Models.DomainModels
{
    public class SortFilterModel
    {
        #region Properties

        public string SortField { get; set; } = Sort.Default;

        public string Catalog { get; set; } = Filter.All;
        public string Type { get; set; } = Filter.All;
        public string Constellation { get; set; } = Filter.All;
        public string Season { get; set; } = Filter.All;
        public string Trajectory { get; set; } = Filter.All;
        public string Visibility { get; set; } = Filter.Any;
        
        public bool IsFilterByHasName { get; set; } = false;

        #endregion
    }
}

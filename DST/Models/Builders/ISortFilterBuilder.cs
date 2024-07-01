using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface ISortFilterBuilder : IBuilder
    {
        SortFilterModel CurrentSortFilter { get; set; }
    }
}

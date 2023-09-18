using DST.Models.Routes;

namespace DST.Models.Builders
{
    public interface ISearchBuilder : IBuilder
    {
        SearchRoute Route { get; set; }
    }
}

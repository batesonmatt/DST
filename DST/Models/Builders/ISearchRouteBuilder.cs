using DST.Models.Routes;

namespace DST.Models.Builders
{
    public interface ISearchRouteBuilder : IBuilder
    {
        SearchRoute Route { get; set; }
    }
}

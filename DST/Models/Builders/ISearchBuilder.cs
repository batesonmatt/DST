using DST.Models.DomainModels;

namespace DST.Models.Builders
{
    public interface ISearchBuilder : IBuilder
    {
        SearchModel CurrentSearch { get; set; }
    }
}
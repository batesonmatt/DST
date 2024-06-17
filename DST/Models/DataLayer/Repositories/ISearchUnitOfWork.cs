using DST.Models.DomainModels;

namespace DST.Models.DataLayer.Repositories
{
    public interface ISearchUnitOfWork
    {
        Repository<DsoModel> DsoItems { get; }
        Repository<DsoTypeModel> DsoTypes { get; }
        Repository<CatalogModel> Catalogs { get; }
        Repository<ConstellationModel> Constellations { get; }
        Repository<SeasonModel> Seasons { get; }
    }
}
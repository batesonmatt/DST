using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using System.Collections.Generic;

namespace DST.Models.DataLayer.Repositories
{
    public interface ISearchUnitOfWork
    {
        Repository<DsoModel> DsoItems { get; }
        Repository<DsoTypeModel> DsoTypes { get; }
        Repository<CatalogModel> Catalogs { get; }
        Repository<ConstellationModel> Constellations { get; }
        Repository<SeasonModel> Seasons { get; }

        IEnumerable<TextValuePair> GetTypeTextValuePairs();
        IEnumerable<TextValuePair> GetCatalogTextValuePairs();
        IEnumerable<TextValuePair> GetConstellationTextValuePairs();
        IEnumerable<TextValuePair> GetSeasonTextValuePairs();
    }
}
using DST.Models.DomainModels;

namespace DST.Models.DataLayer.Repositories
{
    public interface ITrackUnitOfWork
    {
        Repository<DsoModel> DsoItems { get; }

        DsoModel GetDso(string catalog, int id);
        SeasonModel GetSeason(DsoModel dso);
        ConstellationModel GetConstellation(DsoModel dso);
    }
}
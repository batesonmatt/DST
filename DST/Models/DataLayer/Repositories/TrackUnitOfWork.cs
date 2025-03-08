using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DST.Models.DataLayer.Repositories
{
    /// <summary>
    /// A unit of work providing repositories for a read-only MainDbContext.
    /// </summary>
    public class TrackUnitOfWork : ITrackUnitOfWork
    {
        #region Properties

        public Repository<DsoModel> DsoItems
        {
            get
            {
                _dsoData ??= new Repository<DsoModel>(_context);

                return _dsoData;
            }
        }

        #endregion

        #region Fields

        private readonly MainDbContext _context;
        private Repository<DsoModel> _dsoData;

        #endregion

        #region Constructors

        public TrackUnitOfWork(MainDbContext context)
            => _context = context;

        #endregion

        #region Methods

        public DsoModel GetDso(string catalog, int id)
        {
            if (string.IsNullOrWhiteSpace(catalog))
            {
                return null;
            }

            if (id < 1 || id >= int.MaxValue)
            {
                return null;
            }

            DsoModel dso;

            try
            {
                dso = _context.DsoItems
                    .ToList()
                    .AsQueryable()
                    .Where(d => d.CatalogName.EqualsSeo(catalog) && d.Id == id)
                    .FirstOrDefault();
            }
            catch
            {
                dso = null;
            }

            return dso;
        }

        public SeasonModel GetSeason(DsoModel dso)
        {
            if (dso is null || _context is null)
            {
                return null;
            }

            SeasonModel season;

            try
            {
                season = _context.DsoItems
                    .Where(d => d == dso)
                    .Include(i => i.Constellation)
                    .ThenInclude(i => i.Season)
                    .Select(d => d.Constellation.Season)
                    .FirstOrDefault();
            }
            catch
            {
                season = null;
            }

            return season;
        }

        public ConstellationModel GetConstellation(DsoModel dso)
        {
            if (dso is null || _context is null)
            {
                return null;
            }

            ConstellationModel constellation;

            try
            {
                constellation = _context.DsoItems
                    .Where(d => d == dso)
                    .Include(i => i.Constellation)
                    .Select(d => d.Constellation)
                    .FirstOrDefault();
            }
            catch
            {
                constellation = null;
            }

            return constellation;
        }

        #endregion
    }
}

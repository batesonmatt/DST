using DST.Models.DomainModels;

namespace DST.Models.DataLayer.Repositories
{
    /// <summary>
    /// A unit of work providing repositories for a read-only DsoContext.
    /// </summary>
    public class SearchUnitOfWork
    {
        #region Properties

        public Repository<DsoModel> DsoItems
        {
            get
            {
                if (_dsoData == null)
                {
                    _dsoData = new Repository<DsoModel>(_context);
                }

                return _dsoData;
            }
        }

        public Repository<DsoTypeModel> DsoTypes
        {
            get
            {
                if (_typeData == null)
                {
                    _typeData = new Repository<DsoTypeModel>(_context);
                }

                return _typeData;
            }
        }

        public Repository<CatalogModel> Catalogs
        {
            get
            {
                if (_catalogData == null)
                {
                    _catalogData = new Repository<CatalogModel>(_context);
                }

                return _catalogData;
            }
        }

        public Repository<ConstellationModel> Constellations
        {
            get
            {
                if (_constellationData == null)
                {
                    _constellationData = new Repository<ConstellationModel>(_context);
                }

                return _constellationData;
            }
        }

        public Repository<SeasonModel> Seasons
        {
            get
            {
                if (_seasonData == null)
                {
                    _seasonData = new Repository<SeasonModel>(_context);
                }

                return _seasonData;
            }
        }

        #endregion

        #region Fields

        private readonly DsoContext _context;

        private Repository<DsoModel> _dsoData;
        private Repository<DsoTypeModel> _typeData;
        private Repository<CatalogModel> _catalogData;
        private Repository<ConstellationModel> _constellationData;
        private Repository<SeasonModel> _seasonData;

        #endregion

        #region Constructors

        public SearchUnitOfWork(DsoContext context)
            => _context = context;

        #endregion
    }
}

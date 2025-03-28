﻿using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.DataLayer.Repositories
{
    /// <summary>
    /// A unit of work providing repositories for a read-only MainDbContext.
    /// </summary>
    public class SearchUnitOfWork : ISearchUnitOfWork
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

        public Repository<DsoTypeModel> DsoTypes
        {
            get
            {
                _typeData ??= new Repository<DsoTypeModel>(_context);

                return _typeData;
            }
        }

        public Repository<CatalogModel> Catalogs
        {
            get
            {
                _catalogData ??= new Repository<CatalogModel>(_context);

                return _catalogData;
            }
        }

        public Repository<ConstellationModel> Constellations
        {
            get
            {
                _constellationData ??= new Repository<ConstellationModel>(_context);

                return _constellationData;
            }
        }

        public Repository<SeasonModel> Seasons
        {
            get
            {
                _seasonData ??= new Repository<SeasonModel>(_context);

                return _seasonData;
            }
        }

        #endregion

        #region Fields

        private readonly MainDbContext _context;

        private Repository<DsoModel> _dsoData;
        private Repository<DsoTypeModel> _typeData;
        private Repository<CatalogModel> _catalogData;
        private Repository<ConstellationModel> _constellationData;
        private Repository<SeasonModel> _seasonData;

        #endregion

        #region Constructors

        public SearchUnitOfWork(MainDbContext context)
            => _context = context;

        #endregion

        #region Methods

        public IEnumerable<TextValuePair> GetTypeTextValuePairs()
        {
            return DsoTypes
                .List(
                    new QueryOptions<DsoTypeModel>
                    {
                        OrderBy = type => type.Type
                    })
                .Select(
                    type => new TextValuePair(type.Type, type.Type))
                .Prepend(
                    new TextValuePair(Filter.All, Filter.All));
        }

        public IEnumerable<TextValuePair> GetCatalogTextValuePairs()
        {
            return Catalogs
                .List(
                    new QueryOptions<CatalogModel>
                    {
                        OrderBy = catalog => catalog.Name
                    })
                .Select(
                    catalog => new TextValuePair(catalog.Name, catalog.Name))
                .Prepend(
                    new TextValuePair(Filter.All, Filter.All));
        }

        public IEnumerable<TextValuePair> GetConstellationTextValuePairs()
        {
            return Constellations
                .List(
                    new QueryOptions<ConstellationModel>
                    {
                        OrderBy = constellation => constellation.Name
                    })
                .Select(
                    constellation => new TextValuePair(constellation.Name, constellation.Name))
                .Prepend(
                    new TextValuePair(Filter.All, Filter.All));
        }

        public IEnumerable<TextValuePair> GetSeasonTextValuePairs()
        {
            return Seasons
                .List(
                    new QueryOptions<SeasonModel>
                    {
                        OrderBy = season => season.Name
                    })
                .Select(
                    season => new TextValuePair(season.Name, season.Name))
                .Prepend(
                    new TextValuePair(Filter.All, Filter.All));
        }

        #endregion
    }
}

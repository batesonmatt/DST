using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;

namespace DST.Models.Routes
{
    public class TrackSummaryRoute : IRouteDictionary<TrackSummaryRoute>
    {
        #region Properties

        public string Catalog { get; set; } = Filter.Unknown;
        public int Id { get; set; } = 1;
        public string Algorithm { get; set; } = AlgorithmName.Default;

        #endregion

        #region Constructors

        public TrackSummaryRoute() { }

        public TrackSummaryRoute(TrackSummaryRoute values)
        {
            Catalog = values.Catalog;
            Id = values.Id;
            Algorithm = values.Algorithm;
        }

        #endregion

        #region Methods

        public void SetCatalog(string catalog)
        {
            Catalog = string.IsNullOrWhiteSpace(catalog) ? Filter.Unknown : catalog;
        }

        public void SetId(int id)
        {
            Id = Id is < 0 or int.MaxValue ? 0 : id;
        }

        public void SetAlgorithm(string algorithm)
        {
            if (algorithm.EqualsSeo(AlgorithmName.GMST))
            {
                Algorithm = AlgorithmName.GMST;
            }
            else if (algorithm.EqualsSeo(AlgorithmName.GAST))
            {
                Algorithm = AlgorithmName.GAST;
            }
            else if (algorithm.EqualsSeo(AlgorithmName.ERA))
            {
                Algorithm = AlgorithmName.ERA;
            }
            else
            {
                Algorithm = AlgorithmName.Default;
            }
        }

        public TrackSummaryRoute Clone()
        {
            return (TrackSummaryRoute)MemberwiseClone();
        }

        public virtual IDictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { nameof(Catalog), Catalog.ToKebabCase() },
                { nameof(Id), Id.ToString().ToKebabCase() },
                { nameof(Algorithm), Algorithm.ToKebabCase() }
            };
        }

        public virtual void Validate()
        {
            SetCatalog(Catalog);
            SetId(Id);
            SetAlgorithm(Algorithm);
        }

        #endregion
    }
}

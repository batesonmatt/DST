using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DST.Models.Routes
{
    public class TrackPhaseRoute : TrackSummaryRoute, IRouteDictionary<TrackPhaseRoute>
    {
        #region Properties

        public string CoordinateFormat { get; set; } = CoordinateFormatName.Default;
        public long Start { get; set; }
        public string Phase { get; set; } = PhaseName.Default;
        public string TrackOnce { get; set; } = Filter.Off;
        public int Cycles { get; set; }

        [JsonIgnore]
        public bool IsTrackOnce => TrackOnce.IsFilterOn();

        #endregion

        #region Constructors

        public TrackPhaseRoute() { }

        public TrackPhaseRoute(TrackSummaryRoute values) : base(values) { }

        public TrackPhaseRoute(TrackPhaseRoute values)
        {
            Start = values.Start;
            Phase = values.Phase;
            TrackOnce = values.TrackOnce;
            Cycles = values.Cycles;
        }

        #endregion

        #region Methods

        public void SetCoordinateFormat(string format)
        {
            if (format.EqualsSeo(CoordinateFormatName.Component))
            {
                CoordinateFormat = CoordinateFormatName.Component.ToKebabCase();
            }
            else if (format.EqualsSeo(CoordinateFormatName.Decimal))
            {
                CoordinateFormat = CoordinateFormatName.Decimal.ToKebabCase();
            }
            else if (format.EqualsSeo(CoordinateFormatName.Compact))
            {
                CoordinateFormat = CoordinateFormatName.Compact.ToKebabCase();
            }
            else
            {
                CoordinateFormat = CoordinateFormatName.Default.ToKebabCase();
            }
        }

        public void SetPhase(string phase)
        {
            if (phase.EqualsSeo(PhaseName.Rise))
            {
                Phase = PhaseName.Rise.ToKebabCase();
            }
            else if (phase.EqualsSeo(PhaseName.Apex))
            {
                Phase = PhaseName.Apex.ToKebabCase();
            }
            else if (phase.EqualsSeo(PhaseName.Set))
            {
                Phase = PhaseName.Set.ToKebabCase();
            }
            else
            {
                Phase = PhaseName.Default.ToKebabCase();
            }
        }

        public void SetStart(long start)
        {
            Start = start is >= 0 and < long.MaxValue ? start : 0;
        }

        public void SetTrackOnce(bool isTrackOnce)
        {
            TrackOnce = isTrackOnce ? Filter.On : Filter.Off;
        }

        public void SetCycles(int cycles)
        {
            Cycles = !IsTrackOnce && cycles is > -100 and < 100 ? cycles : 0;
        }

        public new TrackPhaseRoute Clone()
        {
            return (TrackPhaseRoute)MemberwiseClone();
        }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> route = new()
            {
                { nameof(CoordinateFormat), CoordinateFormat.ToKebabCase() },
                { nameof(Start), Start.ToString().ToKebabCase() },
                { nameof(Phase), Phase.ToKebabCase() },
                { nameof(TrackOnce), TrackOnce.ToKebabCase() },

                // Do not call ToKebabCase() because we want negative numbers represented in the route URL.
                { nameof(Cycles), Cycles.ToString() }
            };

            return base.ToDictionary()
                .Concat(route)
                .ToDictionary(segment => segment.Key, segment => segment.Value);
        }

        public new void Validate()
        {
            base.Validate();

            SetCoordinateFormat(CoordinateFormat);
            SetStart(Start);
            SetPhase(Phase);
            SetTrackOnce(IsTrackOnce);
            SetCycles(Cycles);
        }

        public TrackPhaseRoute Reset()
        {
            return new TrackPhaseRoute(this as TrackSummaryRoute);
        }

        public TrackSummaryRoute GetSummaryRoute()
        {
            return new TrackSummaryRoute()
            {
                Catalog = Catalog,
                Id = Id,
                Algorithm = Algorithm
            };
        }

        #endregion
    }
}

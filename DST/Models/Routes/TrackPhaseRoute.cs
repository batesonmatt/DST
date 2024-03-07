using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DST.Models.Routes
{
    public class TrackPhaseRoute : TrackSummaryRoute, IRouteDictionary<TrackPhaseRoute>
    {
        #region Properties

        public string Phase { get; set; } = PhaseName.Default;
        public long Start { get; set; }
        public int Cycles { get; set; }

        #endregion

        #region Constructors

        public TrackPhaseRoute() { }

        public TrackPhaseRoute(TrackSummaryRoute values) : base(values) { }

        public TrackPhaseRoute(TrackPhaseRoute values)
        {
            Phase = values.Phase;
            Start = values.Start;
            Cycles = values.Cycles;
        }

        #endregion

        #region Methods

        public void SetPhase(string phase)
        {
            if (phase.EqualsSeo(PhaseName.Rise))
            {
                Phase = PhaseName.Rise;
            }
            else if (phase.EqualsSeo(PhaseName.Apex))
            {
                Phase = PhaseName.Apex;
            }
            else if (phase.EqualsSeo(PhaseName.Set))
            {
                Phase = PhaseName.Set;
            }
            else
            {
                Phase = PhaseName.Default;
            }
        }

        public Core.Trajectory.Phase GetPhase()
        {
            Core.Trajectory.Phase phase;

            if (Phase.EqualsSeo(PhaseName.Rise))
            {
                phase = Core.Trajectory.Phase.Rise;
            }
            else if (Phase.EqualsSeo(PhaseName.Apex))
            {
                phase = Core.Trajectory.Phase.Apex;
            }
            else if (Phase.EqualsSeo(PhaseName.Set))
            {
                phase = Core.Trajectory.Phase.Set;
            }
            else
            {
                phase = Core.Trajectory.Phase.Default;
            }

            return phase;
        }

        public void SetStart(long start)
        {
            Start = start is >= 0 and < long.MaxValue ? start : 0;
        }

        public void SetCycles(int cycles)
        {
            Cycles = cycles is > int.MinValue and < int.MaxValue ? cycles : 0;
        }

        public new TrackPhaseRoute Clone()
        {
            return (TrackPhaseRoute)MemberwiseClone();
        }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> route = new()
            {
                { nameof(Phase), Phase.ToKebabCase() },
                { nameof(Start), Start.ToString().ToKebabCase() },

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
            SetPhase(Phase);
            SetStart(Start);
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

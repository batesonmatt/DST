using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DST.Models.Routes
{
    public class TrackPeriodRoute : TrackSummaryRoute, IRouteDictionary<TrackPeriodRoute>
    {
        #region Properties

        public long Start { get; set; }
        public string TrackOnce { get; set; } = Filter.Off;
        public string Fixed { get; set; } = Filter.Off;
        public string Aggregate { get; set; } = Filter.On;
        public string TimeUnit { get; set; } = TimeUnitName.Default;
        public int Period { get; set; }
        public int Interval { get; set; }

        [JsonIgnore]
        public bool IsTrackOnce => TrackOnce.IsFilterOn();

        [JsonIgnore]
        public bool IsFixed => Fixed.IsFilterOn();

        [JsonIgnore]
        public bool IsAggregated => Aggregate.IsFilterOn();

        #endregion

        #region Constructors

        public TrackPeriodRoute() { }

        public TrackPeriodRoute(TrackSummaryRoute values) : base(values) { }

        public TrackPeriodRoute(TrackPeriodRoute values)
        {
            Start = values.Start;
            TrackOnce = values.TrackOnce;
            Fixed = values.Fixed;
            Aggregate = values.Aggregate;
            TimeUnit = values.TimeUnit;
            Period = values.Period;
            Interval = values.Interval;
        }

        #endregion

        #region Methods

        public bool SupportsFixedTracking()
        {
            return !IsTrackOnce && Utilities.SupportsFixedTracking(Utilities.GetTimeUnit(TimeUnit));
        }

        public bool SupportsAggregatedIntervals()
        {
            return IsFixed && !IsTrackOnce && Utilities.SupportsAggregatedIntervals(Utilities.GetTimeUnit(TimeUnit));
        }

        public void SetStart(long start)
        {
            Start = start is >= 0 and < long.MaxValue ? start : 0;
        }

        public void SetTrackOnce(bool isTrackOnce)
        {
            TrackOnce = isTrackOnce ? Filter.On : Filter.Off;
        }

        public void SetFixed(bool isFixed)
        {
            Fixed = isFixed && SupportsFixedTracking() ? Filter.On : Filter.Off;
        }

        public void SetAggregate(bool isAggregated)
        {
            // Default to On.
            // Only set to Off if Aggregated is supported and the client explicitly disables the option.
            Aggregate = !isAggregated && SupportsAggregatedIntervals() ? Filter.Off : Filter.On;
        }

        public void SetTimeUnit(string timeUnit)
        {
            if (IsTrackOnce)
            {
                TimeUnit = TimeUnitName.Default;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Seconds))
            {
                TimeUnit = TimeUnitName.Seconds;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Minutes))
            {
                TimeUnit = TimeUnitName.Minutes;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Hours))
            {
                TimeUnit = TimeUnitName.Hours;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Days))
            {
                TimeUnit = TimeUnitName.Days;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Weeks))
            {
                TimeUnit = TimeUnitName.Weeks;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Months))
            {
                TimeUnit = TimeUnitName.Months;
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Years))
            {
                TimeUnit = TimeUnitName.Years;
            }
            else
            {
                TimeUnit = TimeUnitName.Default;
            }
        }

        public void SetPeriod(int period)
        {
            // Fixed should already be validated here.
            Period = IsTrackOnce ? 0 : Utilities.GetClientPeriod(period, Algorithm, TimeUnit, IsFixed);
        }

        public void SetInterval(int interval)
        {
            // Period should already be validated here.
            Interval = IsTrackOnce ? 0 : int.Clamp(interval, 0, Period);
        }

        public new TrackPeriodRoute Clone()
        {
            return (TrackPeriodRoute)MemberwiseClone();
        }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> route = new()
            {
                { nameof(Start), Start.ToString().ToKebabCase() },
                { nameof(TrackOnce), TrackOnce.ToKebabCase() },
                { nameof(Fixed), Fixed.ToKebabCase() },
                { nameof(Aggregate), Aggregate.ToKebabCase() },
                { nameof(TimeUnit), TimeUnit.ToKebabCase() },

                // Do not call ToKebabCase() because we want negative numbers represented in the route URL.
                { nameof(Period), Period.ToString() },
                { nameof(Interval), Interval.ToString() }
            };

            return base.ToDictionary()
                .Concat(route)
                .ToDictionary(segment => segment.Key, segment => segment.Value);
        }

        public new void Validate()
        {
            base.Validate();

            SetStart(Start);
            SetTrackOnce(IsTrackOnce);
            SetTimeUnit(TimeUnit);
            SetFixed(IsFixed);
            SetAggregate(IsAggregated);
            SetPeriod(Period);
            SetInterval(Interval);
        }

        public TrackPeriodRoute Reset()
        {
            return new TrackPeriodRoute(this as TrackSummaryRoute);
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

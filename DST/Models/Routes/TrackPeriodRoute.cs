using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DST.Models.Routes
{
    public class TrackPeriodRoute : TrackSummaryRoute, IRouteDictionary<TrackPeriodRoute>
    {
        #region Properties

        public string CoordinateFormat { get; set; } = CoordinateFormatName.Default;
        public long Start { get; set; }
        public string TrackOnce { get; set; } = Filter.Off;
        public string Fixed { get; set; } = Filter.Off;
        public string Aggregate { get; set; } = Filter.Off;
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
            return SupportsFixedTracking() && IsFixed && Utilities.SupportsAggregatedIntervals(Utilities.GetTimeUnit(TimeUnit));
        }

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
            Aggregate = isAggregated && SupportsAggregatedIntervals() ? Filter.On : Filter.Off;
        }

        public void SetTimeUnit(string timeUnit)
        {
            if (IsTrackOnce)
            {
                TimeUnit = TimeUnitName.Default.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Seconds))
            {
                TimeUnit = TimeUnitName.Seconds.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Minutes))
            {
                TimeUnit = TimeUnitName.Minutes.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Hours))
            {
                TimeUnit = TimeUnitName.Hours.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Days))
            {
                TimeUnit = TimeUnitName.Days.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Weeks))
            {
                TimeUnit = TimeUnitName.Weeks.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Months))
            {
                TimeUnit = TimeUnitName.Months.ToKebabCase();
            }
            else if (timeUnit.EqualsSeo(TimeUnitName.Years))
            {
                TimeUnit = TimeUnitName.Years.ToKebabCase();
            }
            else
            {
                TimeUnit = TimeUnitName.Default.ToKebabCase();
            }
        }

        public void SetPeriod(int period)
        {
            // Fixed should already be validated here.
            Period = IsTrackOnce ? 0 : Utilities.GetClientPeriod(period, Algorithm, TimeUnit, IsFixed);
        }

        public void SetInterval(int interval)
        {
            // The interval length must be between 0 and the duration of the period length.
            // Period should already be validated here.
            Interval = IsTrackOnce ? 0 : int.Clamp(interval, 0, Math.Abs(Period));
        }

        public new TrackPeriodRoute Clone()
        {
            return (TrackPeriodRoute)MemberwiseClone();
        }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> route = new()
            {
                { nameof(CoordinateFormat), CoordinateFormat.ToKebabCase() },
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

            SetCoordinateFormat(CoordinateFormat);
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

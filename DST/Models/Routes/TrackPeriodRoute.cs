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
        public string Fixed { get; set; } = Filter.Off;
        public string TimeUnit { get; set; } = TimeUnitName.Default;
        public int Period { get; set; }
        public int Interval { get; set; }

        [JsonIgnore]
        public bool IsFixed => Fixed.IsFilterOn();

        #endregion

        #region Constructors

        public TrackPeriodRoute() { }

        public TrackPeriodRoute(TrackSummaryRoute values) : base(values) { }

        public TrackPeriodRoute(TrackPeriodRoute values)
        {
            Start = values.Start;
            Fixed = values.Fixed;
            TimeUnit = values.TimeUnit;
            Period = values.Period;
            Interval = values.Interval;
        }

        #endregion

        #region Methods

        public bool SupportsFixedTracking()
        {
            return Utilities.SupportsFixedTracking(Utilities.GetTimeUnit(TimeUnit));
        }

        public void SetStart(long start)
        {
            Start = start is >= 0 and < long.MaxValue ? start : 0;
        }

        public void SetFixed(bool isFixed)
        {
            Fixed = isFixed && SupportsFixedTracking() ? Filter.On : Filter.Off;
        }

        public void SetTimeUnit(string timeUnit)
        {
            if (timeUnit.EqualsSeo(TimeUnitName.Seconds))
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
            Period = Utilities.GetClientPeriod(period, Algorithm, TimeUnit, IsFixed);
        }

        public void SetInterval(int interval)
        {
            // Period should already be validated here.
            Interval = int.Clamp(interval, 0, Period);
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
                { nameof(Fixed), Fixed.ToKebabCase() },
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
            SetTimeUnit(TimeUnit);
            SetFixed(Fixed.IsFilterOn());
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

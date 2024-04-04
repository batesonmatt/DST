using DST.Core.DateTimeAdder;
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
        public bool IsFixed => !string.IsNullOrWhiteSpace(Fixed);

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

        public void SetStart(long start)
        {
            Start = start is >= 0 and < long.MaxValue ? start : 0;
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

        public Core.DateTimeAdder.TimeUnit GetTimeUnit()
        {
            Core.DateTimeAdder.TimeUnit timeUnit;

            if (TimeUnit.EqualsSeo(TimeUnitName.Seconds))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Seconds;
            }
            else if (TimeUnit.EqualsSeo(TimeUnitName.Minutes))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Minutes;
            }
            else if (TimeUnit.EqualsSeo(TimeUnitName.Hours))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Hours;
            }
            else if (TimeUnit.EqualsSeo(TimeUnitName.Days))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Days;
            }
            else if (TimeUnit.EqualsSeo(TimeUnitName.Weeks))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Weeks;
            }
            else if (TimeUnit.EqualsSeo(TimeUnitName.Months))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Months;
            }
            else if (TimeUnit.EqualsSeo(TimeUnitName.Years))
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Years;
            }
            else
            {
                timeUnit = Core.DateTimeAdder.TimeUnit.Default;
            }

            return timeUnit;
        }

        public void SetPeriod(int period)
        {
            /* IDateTimeAdder dateTimeAdder = DateTimeAdderFactory.Create() */

            /* 
             * If Algorithm = GMST or GAST, and Fixed = On, use TimeScale.SiderealTime
             * If Algorithm = ERA, and Fixed = On, use TimeScale.StellarTime
             * Otherwise, use TimeScale.MeanSolarTime
             */

            /* Default => 0, Validate based on IDateTimeAdder.Min and .Max values */
        }

        public void SetInterval(int interval)
        {
            /* Default => 0, Validate for >= 0 and <= validated period */
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

            /* If TimeUnit = Seconds, Minutes, or Hours, disable the Fixed option (And set property to "Off") */
            if (!(Fixed.IsFilterOn() || Fixed.IsFilterOff()))
            {
                Fixed = Filter.Off;
            }

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

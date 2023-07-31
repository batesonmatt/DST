using DST.Models.Extensions;
using System;

namespace DST.Models.DataLayer.Query
{
    [Obsolete("Not adding geolocation information to routing service at this time.")]
    public class LongitudeFilter : BaseFilter
    {
        #region Properties

        public override string Id { get; }

        public override string Value => _value;

        public double Longitude => _longitude;

        #endregion

        #region Fields

        private string _value;

        private double _longitude;

        #endregion

        #region Constructors

        public LongitudeFilter()
            : this(string.Empty, 0.0)
        { }

        public LongitudeFilter(string value)
            : this(string.Empty, value)
        { }

        public LongitudeFilter(string id, string value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            _longitude = value.ToLongitude();
            _value = _longitude.ToLongitudeSeo();
        }

        public LongitudeFilter(double value)
            : this(string.Empty, value)
        { }

        public LongitudeFilter(string id, double value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            _value = value.ToLongitudeSeo();
            _longitude = _value.ToLongitude();
        }

        #endregion

        #region Methods

        public override bool IsDefault()
        {
            return _value == GeoIndicator.DefaultLongitude && _longitude == 0.0;
        }

        public override void Reset()
        {
            _value = GeoIndicator.DefaultLongitude;
            _longitude = 0.0;
        }

        public override bool EqualsSeo(string value)
        {
            return _value.EqualsExact(value);
        }

        #endregion
    }
}

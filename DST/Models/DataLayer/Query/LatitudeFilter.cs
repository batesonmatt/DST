using DST.Models.Extensions;
using System;

namespace DST.Models.DataLayer.Query
{
    [Obsolete("Not adding geolocation information to routing service at this time.")]
    public class LatitudeFilter : BaseFilter
    {
        #region Properties

        public override string Id { get; }

        public override string Value => _value;

        public double Latitude => _latitude;

        #endregion

        #region Fields

        private string _value;

        private double _latitude;

        #endregion

        #region Constructors

        public LatitudeFilter()
            : this(string.Empty, 0.0)
        { }

        public LatitudeFilter(string value)
            : this(string.Empty, value)
        { }

        public LatitudeFilter(string id, string value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            _latitude = value.ToLatitude();
            _value = _latitude.ToLatitudeSeo();
        }

        public LatitudeFilter(double value)
            : this(string.Empty, value)
        { }

        public LatitudeFilter(string id, double value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            _value = value.ToLatitudeSeo();
            _latitude = _value.ToLatitude();
        }

        #endregion

        #region Methods

        public override bool IsDefault()
        {
            return _value == GeoIndicator.DefaultLatitude && _latitude == 0.0;
        }

        public override void Reset()
        {
            _value = GeoIndicator.DefaultLatitude;
            _latitude = 0.0;
        }

        public override bool EqualsSeo(string value)
        {
            return _value.EqualsExact(value);
        }

        #endregion
    }
}

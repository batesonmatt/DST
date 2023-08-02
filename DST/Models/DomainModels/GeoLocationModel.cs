using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class GeolocationModel
    {
        #region Properties

        public static string DefaultId { get; } = TimeZoneInfo.Utc.Id;

        public string TimeZoneId { get; set; }

        public string UserTimeZoneId { get; set; }

        /// <summary>The latitudinal component of a geographic coordinate, as represented in decimal degrees.</summary>
        /// <remarks>The value for this property will be rounded to 7 decimal places.</remarks>
        /// <value>A floating-point value ranging from <c>-90.0</c> to <c>90.0</c>.</value>
        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be in the range from -90.0 to 90.0")]
        public double Latitude
        {
            get
            {
                return _latitude;
            }

            set
            {
                _latitude = Math.Round(value, 7);
            }
        }

        /// <summary>The longitudinal component of a geographic coordinate, as represented in decimal degrees.</summary>
        /// <remarks>The value for this property will be rounded to 7 decimal places.</remarks>
        /// <value>A floating-point value ranging from <c>-180.0</c> to <c>180.0</c>.</value>
        [Range(-180.0, 180.0, ErrorMessage = "Longitude must be in the range from -180.0 to 180.0")]
        public double Longitude
        {
            get
            {
                return _longitude;
            }

            set
            {
                _longitude = Math.Round(value, 7);
            }
        }

        #endregion

        #region Fields

        private double _latitude;
        private double _longitude;

        #endregion

        #region Methods

        public void VerifyAndUpdateTimeZone(string id)
        {
            TimeZoneId = GetVerifiedId(id);
            UserTimeZoneId = TimeZoneId;
        }

        public void ResetTimeZone()
        {
            TimeZoneId = DefaultId;
            UserTimeZoneId = DefaultId;
        }

        public void ResetLocation()
        {
            _latitude = 0.0;
            _longitude = 0.0;
        }

        public void Reset()
        {
            ResetTimeZone();
            ResetLocation();
        }

        public bool IsDefaultTimeZone()
        {
            return TimeZoneId == DefaultId && UserTimeZoneId == DefaultId;
        }

        public bool IsDefaultLocation()
        {
            return _latitude== 0.0 && _longitude == 0.0;
        }

        public bool IsDefault()
        {
            return IsDefaultTimeZone() && IsDefaultLocation();
        }

        private static string GetVerifiedId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return DefaultId;
            }

            string result;

            try
            {
                TimeZoneInfo t = TimeZoneInfo.FindSystemTimeZoneById(id);

                if (t.HasIanaId)
                {
                    if (TimeZoneInfo.TryConvertIanaIdToWindowsId(t.Id, out result) == false)
                    {
                        result = DefaultId;
                    }
                }
                else
                {
                    result = t.Id;
                }
            }
            catch
            {
                result = DefaultId;
            }

            return result;
        }

        #endregion
    }
}

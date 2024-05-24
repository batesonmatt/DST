using DST.Core.Coordinate;
using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class GeolocationModel
    {
        #region Properties

        /// <summary>The default time zone identifier.</summary>
        /// <value>A Coordinated Universal Time (UTC) zone identifier.</value>
        public static string DefaultId => TimeZoneInfo.Utc.Id;

        /// <summary>The default GeolocationModel object.</summary>
        /// <value>A geolocation of <c>0°N 0°E</c> and a time zone set to Coordinated Universal Time (UTC).</value>
        public static GeolocationModel Default => new()
        {
            TimeZoneId = DefaultId,
            UserTimeZoneId = DefaultId,
            Latitude = 0.0,
            Longitude = 0.0
        };

        /// <summary>The unique time zone identifier.</summary>
        /// <remarks>
        /// This value may not represent a true time zone ID if updated from a form field.
        /// Use <see cref="VerifyAndUpdateTimeZone(string)"/> to set this property value to a verified ID.
        /// </remarks>
        /// <value>A Windows or IANA time zone ID.</value>
        public string TimeZoneId { get; set; } = string.Empty;

        /// <summary>A secondary, user-defined unique time zone identifier.</summary>
        /// <remarks>
        /// This value may not represent a true time zone ID if updated from a form field.
        /// Use <see cref="VerifyAndUpdateTimeZone(string)"/> to set this property value to a verified ID.
        /// </remarks>
        /// <value>A Windows or IANA time zone ID.</value>
        public string UserTimeZoneId { get; set; } = string.Empty;

        /// <summary>The latitudinal component of a geographic coordinate, as represented in decimal degrees.</summary>
        /// <remarks>The value for this property will be rounded to 7 decimal places.</remarks>
        /// <value>A floating-point value ranging from <c>-90.0</c> to <c>90.0</c>.</value>
        [Range(-90.0, 90.0, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "LatitudeValidationMessage")]
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
        [Range(-180.0, 180.0, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "LongitudeValidationMessage")]
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
            Latitude = 0.0;
            Longitude = 0.0;
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
            return Latitude == 0.0 && Longitude == 0.0;
        }

        public bool IsDefault()
        {
            return IsDefaultTimeZone() && IsDefaultLocation();
        }

        public void SetGeolocation(GeolocationModel other)
        {
            if (other is null)
            {
                return;
            }

            Latitude = other.Latitude;
            Longitude = other.Longitude;

            if (other.TimeZoneId != string.Empty)
            {
                // Verify the selected id.
                VerifyAndUpdateTimeZone(other.TimeZoneId);
            }
            else if (other.UserTimeZoneId != string.Empty)
            {
                // Try to verify the retrieved IANA id.
                VerifyAndUpdateTimeZone(other.UserTimeZoneId);
            }
            else
            {
                // No timezone was selected or found. Default to UTC.
                ResetTimeZone();
            }
        }

        public IGeographicCoordinate GetCoordinate()
        {
            return CoordinateFactory.CreateGeographic(latitude: new(Latitude), longitude: new(Longitude));
        }

        public string GetCoordinateText()
        {
            return GetCoordinate().ToString();
        }

        #endregion
    }
}

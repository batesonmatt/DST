using System;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class GeolocationModel
    {
        #region Properties

        public string TimeZoneName
        {
            get
            {
                return _timeZoneName;
            }

            set
            {
                _timeZoneName = value;
            }
        }

        /// <summary>The latitudinal component of this geographic coordinate, as represented in decimal degrees.</summary>
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

        /// <summary>The longitudinal component of this geographic coordinate, as represented in decimal degrees.</summary>
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

        private string _timeZoneName;
        private double _latitude;
        private double _longitude;

        #endregion
    }
}

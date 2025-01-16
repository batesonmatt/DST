using DST.Core.Coordinate;
using DST.Core.Vector;

namespace DST.Models.BusinessLogic
{
    public class TrackResult
    {
        #region Fields

        private FormatType _format;

        #endregion

        #region Properties

        public ILocalVector Vector { get; }

        #endregion

        #region Constructors

        public TrackResult(ILocalVector vector, FormatType format)
        {
            Vector = vector;
            _format = format;
        }

        #endregion

        #region Methods

        public string GetUniversalDateTimeText()
        {
            return Vector.DateTime.ToString();
        }

        public string GetLocalDateTimeText()
        {
            return Vector.DateTime.ToLocalTime().ToString();
        }

        public string GetAltitudeText()
        {
            return Vector.Position.Format(_format, Core.Components.ComponentType.Inclination);
        }

        public string GetAzimuthText()
        {
            return Vector.Position.Format(_format, Core.Components.ComponentType.Rotation);
        }

        public string GetCoordinateText()
        {
            return Vector.Coordinate.Format(_format);
        }

        #endregion
    }
}

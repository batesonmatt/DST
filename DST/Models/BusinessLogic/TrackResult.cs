using DST.Core.Vector;

namespace DST.Models.BusinessLogic
{
    public class TrackResult
    {
        #region Properties

        public ILocalVector Vector { get; }

        #endregion

        #region Constructors

        public TrackResult(ILocalVector vector)
        {
            Vector = vector;
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
            return Vector.Position.Format(Core.Coordinate.FormatType.Component, Core.Components.ComponentType.Inclination);
        }

        public string GetAzimuthText()
        {
            return Vector.Position.Format(Core.Coordinate.FormatType.Component, Core.Components.ComponentType.Rotation);
        }

        public string GetCoordinateText()
        {
            return Vector.Coordinate.Format(Core.Coordinate.FormatType.Component);
        }

        #endregion
    }
}

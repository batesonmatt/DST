using DST.Models.BusinessLogic;
using System.Collections.Generic;

namespace DST.Models.DataLayer.Query
{
    public static class Sort
    {
        #region Properties

        public static string Id => Resources.DisplayText.SortFieldId;
        public static string Name => Resources.DisplayText.SortFieldName;
        public static string Type => Resources.DisplayText.SortFieldType;
        public static string Constellation => Resources.DisplayText.SortFieldConstellation;
        public static string Distance => Resources.DisplayText.SortFieldDistance;
        public static string Brightness => Resources.DisplayText.SortFieldBrightness;
        public static string RiseTime => Resources.DisplayText.SortFieldRiseTime;

        public static string Default => Id;

        #endregion

        #region Methods

        public static IEnumerable<TextValuePair> GetTextValuePairs()
        {
            return new TextValuePair[]
            {
                new(Id, Id),
                new(Name, Name),
                new(Type, Type),
                new(Constellation, Constellation),
                new(Distance, Distance),
                new(Brightness, Brightness),
                new(RiseTime, RiseTime)
            };
        }

        #endregion
    }
}

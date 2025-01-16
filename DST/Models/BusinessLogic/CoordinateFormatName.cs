using System.Collections.Generic;

namespace DST.Models.BusinessLogic
{
    public static class CoordinateFormatName
    {
        #region Properties

        public static string Component => Resources.DisplayText.CoordinateFormatComponent;
        public static string Decimal => Resources.DisplayText.CoordinateFormatDecimal;
        public static string Compact => Resources.DisplayText.CoordinateFormatCompact;

        public static string Default => Component;

        #endregion

        #region Methods

        public static IEnumerable<TextValuePair> GetTextValuePairs()
        {
            return new TextValuePair[]
            {
                new(Component, Component),
                new(Decimal, Decimal),
                new(Compact, Compact)
            };
        }

        #endregion
    }
}

using System.Collections.Generic;

namespace DST.Models.BusinessLogic
{
    public static class PhaseName
    {
        #region Properties

        public static string Rise => Resources.DisplayText.AltitudeRise;
        public static string Apex => Resources.DisplayText.AltitudeApex;
        public static string Set => Resources.DisplayText.AltitudeSet;

        public static string Default => Rise;

        #endregion

        #region Methods

        public static IEnumerable<TextValuePair> GetTextValuePairs()
        {
            return new TextValuePair[]
            {
                new(Rise, Rise),
                new(Apex, Apex),
                new(Set, Set)
            };
        }

        #endregion
    }
}

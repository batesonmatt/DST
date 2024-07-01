using DST.Models.DataLayer.Query;
using System.Collections.Generic;

namespace DST.Models.BusinessLogic
{
    public static class VisibilityName
    {
        #region Properties

        public static string Local => Resources.DisplayText.FilterLocal;
        public static string Visible => Resources.DisplayText.FilterVisible;
        public static string Rising => Resources.DisplayText.FilterRising;

        public static string Local2 => Resources.DisplayText.FilterLocalDescription;
        public static string Visible2 => Resources.DisplayText.FilterVisibleDescription;
        public static string Rising2 => Resources.DisplayText.FilterRisingDescription;

        #endregion

        #region Methods

        public static IEnumerable<TextValuePair> GetTextValuePairs()
        {
            return new TextValuePair[]
            {
                new(Filter.Any, Filter.Any),
                new(Local2, Local),
                new(Visible2, Visible),
                new(Rising2, Rising)
            };
        }

        #endregion
    }
}

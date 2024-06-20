using DST.Models.DataLayer.Query;
using System.Collections.Generic;

namespace DST.Models.BusinessLogic
{
    public static class TrajectoryName
    {
        #region Properties

        public static string Circumpolar => Resources.DisplayText.TrajectoryCircumpolar;
        public static string NeverRise => Resources.DisplayText.TrajectoryNeverRise;
        public static string RiseAndSet => Resources.DisplayText.TrajectoryRiseAndSet;

        #endregion

        #region Methods

        public static IEnumerable<TextValuePair> GetTextValuePairs()
        {
            return new TextValuePair[]
            {
                new(Filter.All, Filter.All),
                new(Circumpolar, Circumpolar),
                new(NeverRise, NeverRise),
                new(RiseAndSet, RiseAndSet)
            };
        }

        #endregion
    }
}

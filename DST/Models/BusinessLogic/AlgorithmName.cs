using System.Collections.Generic;

namespace DST.Models.BusinessLogic
{
    public static class AlgorithmName
    {
        #region Properties

        public static string GMST => Resources.DisplayText.AlgorithmGMSTShort;
        public static string GAST => Resources.DisplayText.AlgorithmGASTShort;
        public static string ERA => Resources.DisplayText.AlgorithmERAShort;

        public static string GMST2 => Resources.DisplayText.AlgorithmGMSTLong;
        public static string GAST2 => Resources.DisplayText.AlgorithmGASTLong;
        public static string ERA2 => Resources.DisplayText.AlgorithmERALong;

        public static string Default => GMST;

        #endregion

        #region Methods

        public static IEnumerable<TextValuePair> GetTextValuePairs()
        {
            return new TextValuePair[]
            {
                new(GMST2, GMST),
                new(GAST2, GAST),
                new(ERA2, ERA)
            };
        }

        #endregion
    }
}

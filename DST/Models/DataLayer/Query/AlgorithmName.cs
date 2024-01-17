namespace DST.Models.DataLayer.Query
{
    public static class AlgorithmName
    {
        #region Properties

        public static string GMST1 => Resources.DisplayText.AlgorithmGMSTShort;
        public static string GAST1 => Resources.DisplayText.AlgorithmGASTShort;
        public static string ERA1 => Resources.DisplayText.AlgorithmERAShort;

        public static string GMST2 => Resources.DisplayText.AlgorithmGMSTLong;
        public static string GAST2 => Resources.DisplayText.AlgorithmGASTLong;
        public static string ERA2 => Resources.DisplayText.AlgorithmERALong;

        public static string DefaultShort => GMST1;
        public static string DefaultLong => GMST2;

        #endregion
    }
}

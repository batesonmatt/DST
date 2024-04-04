namespace DST.Models.DataLayer.Query
{
    public static class TimeUnitName
    {
        #region Properties

        public static string Seconds => Resources.DisplayText.TimeUnitSeconds;
        public static string Minutes => Resources.DisplayText.TimeUnitMinutes;
        public static string Hours => Resources.DisplayText.TimeUnitHours;
        public static string Days => Resources.DisplayText.TimeUnitDays;
        public static string Weeks => Resources.DisplayText.TimeUnitWeeks;
        public static string Months => Resources.DisplayText.TimeUnitMonths;
        public static string Years => Resources.DisplayText.TimeUnitYears;

        public static string Default => Days;

        #endregion
    }
}

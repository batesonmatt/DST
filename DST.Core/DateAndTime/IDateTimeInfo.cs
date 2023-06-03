namespace DST.Core.DateAndTime
{
    public interface IDateTimeInfo
    {
        TimeZoneInfo ClientTimeZoneInfo { get; }
        bool SupportsDaylightSavingTime { get; }
        TimeSpan BaseUtcOffset { get; }
        DateTime MinStandardDateTime { get; }
        DateTime MaxStandardDateTime { get; }
        IMutableDateTime MinAstronomicalDateTime { get; }
        IMutableDateTime MaxAstronomicalDateTime { get; }
        IDateTime Now { get; }
        IDateTime Today { get; }
        IMutableDateTime ConvertTimeFromStandard(DateTime dateTime);
    }
}
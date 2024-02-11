namespace DST.Core.DateAndTime
{
    public interface IMutableDateTime : IBaseDateTime
    {
        long Ticks { get; }
        long MinTickSpan { get; }
        long MaxTickSpan { get; }

        bool IsMinValue();
        bool IsMaxValue();
        bool IsMinOrMaxValue();
        IMutableDateTime AddSeconds(double value);
        IMutableDateTime AddMinutes(double value);
        IMutableDateTime AddHours(double value);
        IMutableDateTime AddDays(double value);
        IMutableDateTime AddWeeks(double value);
        IMutableDateTime AddMonths(int value);
        IMutableDateTime AddYears(int value);
        IMutableDateTime AddTicks(long value);
        DateTime ToLocalTime();
        DateTime ToStandardTime();
    }
}

namespace DST.Core.DateAndTime
{
    public interface IClientDateTime : IBaseDateTime, IDateTime
    {
        IDateTimeInfo Info { get; }
    }
}

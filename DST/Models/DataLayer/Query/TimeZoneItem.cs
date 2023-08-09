namespace DST.Models.DataLayer.Query
{
    public class TimeZoneItem
    {
        #region Properties

        public string Id { get; }

        public string Name { get; }

        #endregion

        #region Constructors

        public TimeZoneItem(string id, string name)
        {
            Id = string.IsNullOrWhiteSpace(id) ? string.Empty : id;
            Name = string.IsNullOrWhiteSpace(name) ? string.Empty : name;
        }

        #endregion
    }
}

using System;

namespace DST.Models.DataLayer.Query
{
    public class TimeZoneItem
    {
        #region Properties

        public static string DefaultId { get; } = TimeZoneInfo.Utc.Id;

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

        #region Methods

        public static string GetVerifiedId(string id)
        {
            string result;

            try
            {
                TimeZoneInfo t = TimeZoneInfo.FindSystemTimeZoneById(id);
                
                if (t.HasIanaId)
                {
                    if (TimeZoneInfo.TryConvertIanaIdToWindowsId(t.Id, out result) == false)
                    {
                        result = DefaultId;
                    }
                }
                else
                {
                    result = t.Id;
                }
            }
            catch
            {
                result = DefaultId;
            }

            return result;
        }

        #endregion
    }
}

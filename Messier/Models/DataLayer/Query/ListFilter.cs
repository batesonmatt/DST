using System;

namespace DST.Models.DataLayer.Query
{
    public class ListFilter : IFilter
    {
        #region Properties

        public string Id { get; }

        public string Value
        {
            get => _value;

            set => _value = value ?? All;
        }

        public static string All { get; } = "all";

        #endregion

        #region Fields

        private string _value;

        #endregion

        #region Constructors

        public ListFilter()
        {
            Id = Guid.NewGuid().ToString();

            _value = All;
        }

        public ListFilter(string value)
        {
            Id = Guid.NewGuid().ToString();

            _value = value ?? All;
        }

        public ListFilter(string id, string value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;

            _value = value ?? All;
        }

        #endregion

        #region Methods

        public override string ToString() => Value;

        public bool IsDefault() => _value == All;

        public void Reset() => _value = All;

        #endregion
    }
}

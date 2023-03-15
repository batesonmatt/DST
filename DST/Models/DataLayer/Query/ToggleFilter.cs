using System;

namespace DST.Models.DataLayer.Query
{
    public class ToggleFilter : BaseFilter
    {
        #region Properties

        public override string Id { get; }

        public override string Value => _value;

        public static string On { get; } = _on;

        public static string Off { get; } = _off;

        #endregion

        #region Fields

        private string _value;

        private const string _on = "on";

        private const string _off = "off";

        #endregion

        #region Constructors

        public ToggleFilter()
            : this(string.Empty, Off)
        { }

        public ToggleFilter(string value)
            : this(string.Empty, value)
        { }

        public ToggleFilter(string id, string value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            _value = string.IsNullOrWhiteSpace(value) ? Off : value;
        }

        #endregion

        #region Methods

        public override bool IsDefault() => _value == Off;

        public override void Reset() => _value = Off;

        public void Toggle() => _value = _value == Off ? On : Off;

        #endregion
    }
}

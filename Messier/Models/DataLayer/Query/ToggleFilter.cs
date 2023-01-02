using System;

namespace Messier.Models.DataLayer.Query
{
    public class ToggleFilter : IFilter
    {
        #region Properties

        public string Id { get; }

        public string Value
        {
            get => _value;

            set
            {
                switch (value)
                {
                    case _on:
                    case _off:
                        _value = value;
                        break;

                    default:
                        _value = Off;
                        break;
                }
            }
        }

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
        {
            Id = Guid.NewGuid().ToString();

            _value = Off;
        }

        public ToggleFilter(string value)
        {
            Id = Guid.NewGuid().ToString();

            _value = value ?? Off;
        }

        public ToggleFilter(string id, string value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;

            _value = value ?? Off;
        }

        #endregion

        #region Methods

        public override string ToString() => Value;

        public bool IsDefault() => _value == Off;

        public void Reset() => _value = Off;

        public void Toggle() => _value = _value == Off ? On : Off;

        #endregion
    }
}

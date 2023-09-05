using DST.Models.Extensions;
using System;

namespace DST.Models.DataLayer.Query
{
    public class ListFilter : BaseFilter
    {
        #region Properties

        public override string Id { get; }

        public override string Value => _value;

        public static string All => "all";

        #endregion

        #region Fields

        private string _value;

        #endregion

        #region Constructors

        public ListFilter() 
            : this(string.Empty, All)
        { }

        public ListFilter(string value)
            : this(string.Empty, value)
        { }

        public ListFilter(string id, string value)
        {
            Id = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            _value = string.IsNullOrWhiteSpace(value) ? All : value.ToKebabCase();
        }

        #endregion

        #region Methods

        public override bool IsDefault() => _value == All;

        public override void Reset() => _value = All;

        #endregion
    }
}

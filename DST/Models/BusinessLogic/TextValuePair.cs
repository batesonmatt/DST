using DST.Models.Extensions;

namespace DST.Models.BusinessLogic
{
    public class TextValuePair
    {
        #region Properties

        public string Text { get; }
        public string Value { get; }

        #endregion

        #region Constructors

        public TextValuePair(string text, string value)
        {
            Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
            Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value.ToKebabCase();
        }

        #endregion
    }
}

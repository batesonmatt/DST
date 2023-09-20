namespace DST.Models.BusinessLogic
{
    public class PageSizeItem
    {
        #region Properties

        public int Size { get; }
        public string Text { get; }

        #endregion

        #region Constructors

        public PageSizeItem(int size, string text)
        {
            Size = int.Clamp(size, 0, int.MaxValue);
            Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        #endregion
    }
}

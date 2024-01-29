namespace DST.Models.BusinessLogic
{
    public class TrackAlgorithmItem
    {
        #region Properties

        public string Id { get; set; }

        public string Text { get; set; }

        #endregion

        #region Constructors

        public TrackAlgorithmItem(string id, string text)
        {
            Id = string.IsNullOrWhiteSpace(id) ? string.Empty : id;
            Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        #endregion
    }
}

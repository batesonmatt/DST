namespace DST.Models.BusinessLogic
{
    public class TrackPhaseItem
    {
        #region Properties

        public string Id { get; set; }

        public string Text { get; set; }

        #endregion

        #region Constructors

        public TrackPhaseItem(string id, string text)
        {
            Id = string.IsNullOrWhiteSpace(id) ? string.Empty : id;
            Text = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        #endregion
    }
}

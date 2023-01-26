namespace DST.Models.DataLayer.Query
{
    public interface IFilter
    {
        #region Properties

        string Id { get; }

        string Value { get; set; }

        #endregion

        #region Methods

        bool IsDefault();

        void Reset();

        #endregion
    }
}

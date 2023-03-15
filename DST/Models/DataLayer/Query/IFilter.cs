namespace DST.Models.DataLayer.Query
{
    public interface IFilter
    {
        #region Properties

        string Id { get; }

        string Value { get; }

        #endregion

        #region Methods

        bool IsDefault();

        void Reset();

        bool EqualsSeo(string value);

        #endregion
    }
}

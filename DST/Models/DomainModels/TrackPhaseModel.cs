using DST.Models.DataLayer.Query;
using System;

namespace DST.Models.DomainModels
{
    public class TrackPhaseModel
    {
        #region Properties

        public string Phase { get; set; } = PhaseName.Default;

        public DateTime? Start { get; set; }

        public int Cycles { get; set; } = 1;

        #endregion

        #region Methods

        public long GetTicks()
        {
            return Start is null ? 0 : (long)Start?.Ticks;
        }

        public string GetFormattedStart()
        {
            return Start?.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        #endregion
    }
}

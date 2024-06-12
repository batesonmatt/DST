using DST.Core.TimeScalable;
using System.Collections.Generic;
using System;
using System.Collections;

namespace DST.Models.BusinessLogic
{
    public class TrackResults : IEnumerable<TrackResult>
    {
        #region Properties

        public string TimeScaleName => _timeScale.ToString();

        #endregion

        #region Fields

        private readonly List<TrackResult> _results;
        private readonly ITimeScalable _timeScale;

        #endregion

        #region Constructors

        public TrackResults()
        {
            _results = new();
            _timeScale = TimeScalableFactory.Create(TimeScale.Default);
        }

        public TrackResults(IEnumerable<TrackResult> results, TimeScale timeScale)
        {
            _ = results ?? throw new ArgumentNullException(nameof(results));

            _results = new(results);
            _timeScale = TimeScalableFactory.Create(timeScale);
        }

        #endregion

        #region Methods

        public IEnumerator<TrackResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}

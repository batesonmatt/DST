using DST.Models.Builders;
using DST.Models.BusinessLogic;
using DST.Models.DataLayer.Query;
using DST.Models.DomainModels;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DST.Components
{
    public class DsoSortTagViewComponent : ViewComponent
    {
        #region Fields

        private readonly IGeolocationBuilder _geoBuilder;

        #endregion

        #region Constructors

        public DsoSortTagViewComponent(IGeolocationBuilder geoBuilder)
        {
            _geoBuilder = geoBuilder;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(DsoModel dso, string sortField)
        {
            string sortTag = string.Empty;

            if (sortField is not null && dso is not null)
            {
                if (sortField.EqualsSeo(Sort.Type))
                {
                    sortTag = dso.Type ?? string.Empty;
                }
                else if (sortField.EqualsSeo(Sort.Constellation))
                {
                    sortTag = dso.ConstellationName ?? string.Empty;
                }
                else if (sortField.EqualsSeo(Sort.Distance))
                {
                    sortTag = string.Format(Resources.DisplayText.DistanceFormatDecimalKly, dso.Distance);
                }
                else if (sortField.EqualsSeo(Sort.Brightness))
                {
                    sortTag = dso.Magnitude switch
                    {
                        null => Resources.DisplayText.ApparentMagnitudeDefault,
                        _ => string.Format(Resources.DisplayText.ApparentMagnitudeFormatDecimal, dso.Magnitude)
                    };
                }
                else if (sortField.EqualsSeo(Sort.RiseTime))
                {
                    long ticks;
                    TimeSpan timeSpan;

                    try
                    {
                        // Load the client geolocation, if any.
                        _geoBuilder.Load();

                        // Calculate the object's recent/next rise time relative to the observer's geolocation on the
                        // current date and time for the client.
                        ticks = Utilities.GetRiseTime(dso, _geoBuilder.CurrentGeolocation);

                        if (ticks is long.MinValue or long.MaxValue)
                        {
                            sortTag = Resources.DisplayText.RiseTimeOutOfRange;
                        }
                        else
                        {
                            // Get the rise time duration as a timespan.
                            timeSpan = TimeSpan.FromTicks(ticks);

                            // Format the result.
                            if (timeSpan.Days > 0 || timeSpan.Hours > 0)
                            {
                                sortTag = string.Format(Resources.DisplayText.RiseTimeFormatHoursFuture, timeSpan.TotalHours);
                            }
                            else if (timeSpan.Minutes > 0)
                            {
                                sortTag = string.Format(Resources.DisplayText.RiseTimeFormatMinutesFuture, timeSpan.Minutes);
                            }
                            else if (timeSpan.Ticks >= 0)
                            {
                                sortTag = string.Format(Resources.DisplayText.RiseTimeFormatSecondsFuture, timeSpan.Seconds);
                            }
                            else if (timeSpan.Days < 0 || timeSpan.Hours < 0)
                            {
                                sortTag = string.Format(Resources.DisplayText.RiseTimeFormatHoursPast, timeSpan.Duration().TotalHours);
                            }
                            else if (timeSpan.Minutes < 0)
                            {
                                sortTag = string.Format(Resources.DisplayText.RiseTimeFormatMinutesPast, timeSpan.Duration().Minutes);
                            }
                            else
                            {
                                sortTag = string.Format(Resources.DisplayText.RiseTimeFormatSecondsPast, timeSpan.Duration().Seconds);
                            }
                        }
                    }
                    catch
                    {
                        sortTag = Resources.DisplayText.RiseTimeDefault;
                    }
                }
            }

            return View("~/Views/Shared/_DsoSortTagPartial.cshtml", sortTag);
        }

        #endregion
    }
}

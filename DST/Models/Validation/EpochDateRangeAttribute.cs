using DST.Core.DateAndTime;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using DST.Models.Builders;

namespace DST.Models.Validation
{
    public class EpochDateRangeAttribute : ValidationAttribute
    {
        #region Properties

        protected int Years => 1000;

        #endregion

        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result;
            IGeolocationBuilder geoBuilder;
            IDateTimeInfo dateTimeInfo;
            IMutableDateTime epoch;
            DateTime minLocal;
            DateTime maxLocal;

            try
            {
                geoBuilder = validationContext.GetService<IGeolocationBuilder>();
                geoBuilder.Load();

                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geoBuilder.CurrentGeolocation.TimeZoneId);

                epoch = DateTimeFactory.CreateMutable(DateTimeConstants.Epoch.Date, dateTimeInfo);
                //minLocal = epoch.AddYears(-Years).ToLocalTime();
                //maxLocal = epoch.AddYears(Years).ToLocalTime();
                minLocal = DateTimeFactory.CreateMutable(DateTimeConstants.MinUtcDateTime, dateTimeInfo).ToLocalTime();
                maxLocal = DateTimeFactory.CreateMutable(DateTimeConstants.MaxUtcDateTime, dateTimeInfo).ToLocalTime();

                if (value is DateTime dateTime)
                {
                    if (dateTime >= minLocal && dateTime <= maxLocal)
                    {
                        return ValidationResult.Success;
                    }
                }

                string message = base.ErrorMessage ??
                        $"The start date must be between {minLocal.ToShortDateString()} and {maxLocal.ToShortDateString()} for the current time zone.";

                result = new ValidationResult(message);
            }
            catch
            {
                result = new ValidationResult("A problem occurred during validation.");
            }

            return result;
        }

        #endregion
    }
}

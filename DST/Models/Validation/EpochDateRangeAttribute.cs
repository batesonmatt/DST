using DST.Core.DateAndTime;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using DST.Models.Builders;
using System.Globalization;

namespace DST.Models.Validation
{
    public class EpochDateRangeAttribute : ValidationAttribute
    {
        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result;
            IGeolocationBuilder geoBuilder;
            IDateTimeInfo dateTimeInfo;
            DateTime minLocal;
            DateTime maxLocal;

            try
            {
                geoBuilder = validationContext.GetService<IGeolocationBuilder>();
                geoBuilder.Load();

                dateTimeInfo = DateTimeInfoFactory.CreateFromTimeZoneId(geoBuilder.CurrentGeolocation.TimeZoneId);

                minLocal = dateTimeInfo.MinAstronomicalDateTime.ToLocalTime();
                maxLocal = dateTimeInfo.MaxAstronomicalDateTime.ToLocalTime();

                if (value is DateTime dateTime)
                {
                    if (dateTime >= minLocal && dateTime <= maxLocal)
                    {
                        return ValidationResult.Success;
                    }
                }

                string message = base.ErrorMessage ??
                        $"The start date must be between {minLocal.ToString(CultureInfo.CurrentCulture)} and {maxLocal.ToString(CultureInfo.CurrentCulture)} for the current time zone.";

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

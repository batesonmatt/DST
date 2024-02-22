using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using DST.Models.Builders;
using DST.Models.BusinessLogic;

namespace DST.Models.Validation
{
    public class EpochDateRangeAttribute : ValidationAttribute
    {
        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IGeolocationBuilder geoBuilder = validationContext.GetService<IGeolocationBuilder>();
            geoBuilder.Load();

            if (value is DateTime dateTime)
            {
                string message = Utilities.ValidateClientDateTime(dateTime, geoBuilder.CurrentGeolocation);

                return string.IsNullOrWhiteSpace(message) ? ValidationResult.Success : new ValidationResult(message);
            }

            return new ValidationResult(Resources.DisplayText.StartDateValidation);
        }

        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class SearchModel
    {
        #region Properties

        public const int MaxInputLength = 50;

        /// <summary>The search input text.</summary>
        /// <remarks>This value may contain extended letter characters.</remarks>
        /// <value>An alpha-numeric string of 50 characters or less.</value>
        [MaxLength(MaxInputLength, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "SearchLengthValidationMessage")]
        [RegularExpression("^[ 0-9a-zA-Z\\p{L}]+$", ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "SearchRegexValidationMessage")]
        public string Input { get; set; } = string.Empty;

        #endregion
    }
}

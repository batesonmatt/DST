using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DST.Models.DomainModels
{
    public class SearchModel
    {
        #region Properties

        /// <summary>The search input text.</summary>
        /// <remarks>This value may contain extended letter characters.</remarks>
        /// <value>An alpha-numeric string of 50 characters or less.</value>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "SearchRequiredValidationMessage")]
        [MaxLength(MaxInputLength, ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "SearchLengthValidationMessage")]
        [RegularExpression("^[ 0-9a-zA-Z\\p{L}]+$", ErrorMessageResourceType = typeof(Resources.DisplayText), ErrorMessageResourceName = "SearchRegexValidationMessage")]
        public string Input
        {
            get
            {
                return _input;
            }

            set
            {
                if (_input != value)
                {
                    _input = value.Trim();
                }
            }
        }

        /// <summary>
        /// Indicates whether this <see cref="SearchModel.Input"/> property is <see langword="null"/>, empty, 
        /// or consists of only white-space characters.
        /// </summary>
        /// <remarks>This property does not support model binding.</remarks>
        [BindNever]
        public bool IsEmpty => string.IsNullOrWhiteSpace(Input);

        #endregion

        #region Fields

        public const int MaxInputLength = 50;
        private string _input;

        #endregion

        #region Methods

        public void Clear()
        {
            Input = string.Empty;
        }

        #endregion
    }
}

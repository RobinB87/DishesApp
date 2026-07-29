using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyDishesApp.Service.Dtos.Base
{
    /// <summary>
    /// The dish dto base class
    /// </summary>
    public abstract class DishAbstractBaseDto : IValidatableObject
    {
        // Validation is necessary at various level, especially when apps grow. It helps stability of code and data integrity.
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Name is required.")]
        [MaxLength(30, ErrorMessage = "maxLength|Name is too long.")]
        public string Name { get; set; }
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Recipe is required.")]
        [MinLength(30, ErrorMessage = "minLength|Recipe is too short.")]
        public string Recipe { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == Country)
            {
                yield return new ValidationResult(
                "dishNameEqualsCountry|A dish name should be different from the country.",
                new[] { "Dish" });
            }
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace MyDishesApp.Service.Dtos.Base
{
    /// <summary>
    /// The ingredient dto base class
    /// </summary>
    public abstract class IngredientAbstractBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Name is required.")]
        [MaxLength(30, ErrorMessage = "maxLength|Name is too long.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Unit price is required.")]
        [Range(0.001, 999.99, ErrorMessage = "range|Unit price should be a numeric value.")]
        public double PricePerUnit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Quantity is required.")]
        [Range(0.001, 999.99, ErrorMessage = "range|Unit price should be a numeric value.")]
        public double Quantity { get; set; }
    }
}


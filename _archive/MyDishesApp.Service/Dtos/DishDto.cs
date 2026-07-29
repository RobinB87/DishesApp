using MyDishesApp.Service.Dtos.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyDishesApp.Service.Dtos
{
    public class DishDto : DishAbstractBaseDto, IValidatableObject
    {
        public int DishId { get; set; }

        public ICollection<IngredientDto> Ingredients { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<string> ingredientNames = new List<string> { };
            foreach (IngredientDto ingredient in Ingredients)
            {
                if (ingredientNames.IndexOf(ingredient.Name) > -1)
                {
                        yield return new ValidationResult(
                        "duplicateIngredient|Ingredient already exists for this dish. Please choose a different one.",
                        new[] { "Dish" });
                    yield break;
                }
                ingredientNames.Add(ingredient.Name);
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDishesApp.Repository.Data.Entities
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double PricePerUnit { get; set; }

        public virtual ICollection<DishIngredient> DishIngredients { get; set; }
    }
}

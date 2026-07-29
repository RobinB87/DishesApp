using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDishesApp.Repository.Data.Entities
{
    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DishId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        public string Recipe { get; set; }

        public virtual ICollection<DishIngredient> DishIngredients { get; } = new List<DishIngredient>();
    }
}

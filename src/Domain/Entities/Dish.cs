using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Dish
    {
        [Key]
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string Recipe { get; private set; }
        public virtual ICollection<DishIngredient> DishIngredients { get; } = new List<DishIngredient>();

        public Dish(string name, string country, string recipe, Guid guid)
        {
            Guid = guid;
            Name = name;
            Country = country;
            Recipe = recipe;
        }
    }
}

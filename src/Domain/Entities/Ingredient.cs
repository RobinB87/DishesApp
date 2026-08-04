using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; private set; }
        public Guid? Guid { get; private set; }
        public string Name { get; private set; }
        public double PricePerUnit { get; private set; }
        public virtual ICollection<DishIngredient> DishIngredients { get; } = new List<DishIngredient>();

        public Ingredient(string name, double pricePerUnit, Guid? guid = null)
        {
            Guid = guid;
            Name = name;
            PricePerUnit = pricePerUnit;
        }
    }
}

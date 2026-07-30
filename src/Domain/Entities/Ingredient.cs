namespace Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double PricePerUnit { get; private set; }
        public virtual ICollection<DishIngredient> DishIngredients { get; } = new List<DishIngredient>();

        public Ingredient(string name, double pricePerUnit)
        {
            Name = name;
            PricePerUnit = pricePerUnit;
        }
    }
}

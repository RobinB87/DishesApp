namespace Domain.Entities
{
    public class Dish
    {
        public int DishId { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string Recipe { get; private set; }
        public virtual ICollection<DishIngredient> DishIngredients { get; } = new List<DishIngredient>();

        public Dish(string name, string country, string recipe)
        {
            Name = name;
            Country = country;
            Recipe = recipe;
        }
    }
}

namespace MyDishesApp.Repository.Data.Entities
{
    /// <summary>
    /// The class required to create a join table.
    /// With EF5 this is not necessary anymore.
    /// </summary>
    public class DishIngredient
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int IngredientId { get; set; }
        public double Quantity { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}

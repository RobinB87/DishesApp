namespace Domain.Entities
{
    public class DishIngredient
    {
        public int DishId { get; private set; }
        public Dish Dish { get; private set; }
        public int IngredientId { get; private set; }
        public double Quantity { get; private set; }
        public Ingredient Ingredient { get; private set; }

        public DishIngredient(Dish dish, Ingredient ingredient, double quantity)
        {
            Dish = dish;
            DishId = dish.DishId;
            Ingredient = ingredient;
            IngredientId = ingredient.Id;
            Quantity = quantity;
        }
    }
}

using MyDishesApp.Repository.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using MyDishesApp.Repository.Data.Entities.Auth;

namespace MyDishesApp.Repository.Data
{
    public static class DishesContextExtensions
    {
        public static void EnsureSeedDataForContext(this DishesContext context)
        {
            // Check if data is present in users db
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Robin",
                        Password = "1234",
                        Email = "rob@rob.com",
                        LastName = "Robin",
                        PasswordSalt = "salty#saerqeegxxXEGlt!@#saltttt!$(@$*@$*@$*",
                        Role = "Admin"
                    },
                    new User
                    {
                        FirstName = "TestUser",
                        Password = "1234",
                        Email = "test@test.com",
                        LastName = "TestUser",
                        PasswordSalt = "sal12312312456ZZxswraty#salt!@#sa124124ltttt!$(@$*@$*@$*",
                        Role = "User"
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }


            // Check if this data is already in db
            if (context.Dishes.Any())
            {
                return;
            }

            // Initial seed data
            var dishes = new List<Dish>
            {
                new Dish
                {
                    DishId = 1,
                    Name = "Chicken curry",
                    Country = "India",
                    Recipe = "Fruit de ui en de knoflook zacht...",
                },

                new Dish
                {
                    DishId = 2,
                    Name = "Rendang",
                    Country = "Indonesië",
                    Recipe = "Fruit de ui en de knoflook zacht..."
                },
                new Dish
                {
                    DishId = 3,
                    Name = "Pasta Bolognese",
                    Country = "Italy",
                    Recipe = "Bak de saucijzen goed aan..."
                }
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    IngredientId = 1,
                    Name = "Chicken thigh",
                    PricePerUnit = 2.50
                },
                new Ingredient
                {
                    IngredientId = 2,
                    Name = "Brocolli",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 3,
                    Name = "Cooking cream",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 4,
                    Name = "Garlic",
                    PricePerUnit = 0.10
                },
                new Ingredient
                {
                    IngredientId = 5,
                    Name = "Union",
                    PricePerUnit = 0.25
                },
                new Ingredient
                {
                    IngredientId = 6,
                    Name = "Indian mild curry powder",
                    PricePerUnit = 0.25
                },
                new Ingredient
                {
                    IngredientId = 7,
                    Name = "Beef",
                    PricePerUnit = 5.00
                },
                new Ingredient
                {
                    IngredientId = 8,
                    Name = "Beans",
                    PricePerUnit = 2.00
                },
                new Ingredient
                {
                    IngredientId = 9,
                    Name = "Cocos cream",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 10,
                    Name = "Rendang paste",
                    PricePerUnit = 2.00
                },
                new Ingredient
                {
                    IngredientId = 11,
                    Name = "Sausage",
                    PricePerUnit = 2.50
                },
                new Ingredient
                {
                    IngredientId = 12,
                    Name = "Tomatos",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 13,
                    Name = "Red wine",
                    PricePerUnit = 5.00
                },
                new Ingredient
                {
                    IngredientId = 14,
                    Name = "Oregano",
                    PricePerUnit = 0.25
                },
                new Ingredient
                {
                    IngredientId = 15,
                    Name = "Basilicum",
                    PricePerUnit = 0.25
                }
            };

            var dishIngredients = new List<DishIngredient>
            {
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 1,
                    Quantity = 2
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 2,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 3,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 4,
                    Quantity = 6
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 5,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 6,
                    Quantity = 0.25
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 4,
                    Quantity = 6
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 5,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 7,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 8,
                    Quantity = 3
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 9,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 10,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 4,
                    Quantity = 4
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 5,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 11,
                    Quantity = 8
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 12,
                    Quantity = 10
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 13,
                    Quantity = 0.50
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 14,
                    Quantity = 0.25
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 15,
                    Quantity = 0.25
                }
            };

            // Add dishes and ingredients to dishingredients
            foreach (var dishIngredient in dishIngredients)
            {
                dishIngredient.Dish = dishes.FirstOrDefault(d => d.DishId == dishIngredient.DishId);
                dishIngredient.Ingredient = ingredients.FirstOrDefault(i => i.IngredientId == dishIngredient.IngredientId);
            }

            // Add the dishes to the database
            // Store the added dish separately to use its saved id
            var addedDishes = new List<Dish>();
            foreach (var dish in dishes)
            {
                var dishToAdd = CreateDishForDbAdd(dish);
                context.Dishes.Add(dishToAdd);
                context.SaveChanges();

                addedDishes.Add(dishToAdd);
            }

            // Add the ingredients to the database
            // Store the added ingredient separately to use its saved id
            var addedIngredients = new List<Ingredient>();
            foreach (var ingredient in ingredients)
            {
                var ingredientToAdd = CreateIngredientForDbAdd(ingredient);
                context.Ingredients.Add(ingredientToAdd);
                context.SaveChanges();

                addedIngredients.Add(ingredientToAdd);
            }

            // Add the dish ingredients (link table) to the database
            foreach (var dishIngredient in dishIngredients)
            {
                var dishId = addedDishes.Where(d => d.Name == dishIngredient.Dish.Name).Select(d => d.DishId).FirstOrDefault();
                var ingredientId = addedIngredients.Where(i => i.Name == dishIngredient.Ingredient.Name).Select(i => i.IngredientId).FirstOrDefault();

                context.DishIngredients.Add(CreateDishIngredientForDbAdd(dishIngredient, dishId, ingredientId));
            }

            context.SaveChanges();
        }

        private static Ingredient CreateIngredientForDbAdd(Ingredient ingredient)
        {
            return new Ingredient
            {
                Name = ingredient.Name,
                PricePerUnit = ingredient.PricePerUnit
            };
        }

        private static Dish CreateDishForDbAdd(Dish dish)
        {
            return new Dish
            {
                Name = dish.Name,
                Country = dish.Country,
                Recipe = dish.Recipe
            };
        }

        private static DishIngredient CreateDishIngredientForDbAdd(DishIngredient dishIngredient, int dishId, int ingredientId)
        {
            return new DishIngredient
            {
                Quantity = dishIngredient.Quantity,
                DishId = dishId,
                IngredientId = ingredientId
            };
        }
    }
}
import { Ingredient } from "../ingredients/shared/ingredient.model";
import { DishAbstractBase } from "./dish-abstact-base.model";

export class Dish extends DishAbstractBase {
  dishId: number;
  ingredients: Ingredient[];
} 

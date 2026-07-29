import { Ingredient } from '../ingredient';
import { DishAbstractBase } from './dish-abstract-base.model';

export class Dish extends DishAbstractBase {
  dishId: number;
  ingredients: Ingredient[];
}

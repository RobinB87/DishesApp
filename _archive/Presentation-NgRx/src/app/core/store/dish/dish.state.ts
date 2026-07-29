import { Dish } from '../../models/dish';

export interface IDishState {
  dishes: Dish[];
  selectedDish: Dish;
}

export const initialDishState: IDishState = {
  dishes: null,
  selectedDish: null,
};

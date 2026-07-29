import { createSelector } from '@ngrx/store';
import { IAppState } from '../app.state';
import { IDishState } from './dish.state';

const selectDishes = (state: IAppState) => state.dishes;

export const selectDishList = createSelector(
  selectDishes,
  (state: IDishState) => state.dishes
);

export const selectSelectedDish = createSelector(
  selectDishes,
  (state: IDishState) => state.selectedDish
);

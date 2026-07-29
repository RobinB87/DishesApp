import { Action } from '@ngrx/store';
import { Dish } from '../../models/dish';

export enum EDishActions {
  GetDishes = '[Dish] Get Dishes',
  GetDishesSuccess = '[Dish] Get Dishes Success',

  GetDish = '[Dish] Get Dish',
  GetDishSuccess = '[Dish] Get Dish Success',

  Add = '[Dish] Add',
  Remove = '[Dish] Remove',
}

// export class ActionEx implements Action {
//   readonly type;
//   payload: any;
// }

export class GetDishes implements Action {
  public readonly type = EDishActions.GetDishes;
}

export class GetDishesSuccess implements Action {
  public readonly type = EDishActions.GetDishesSuccess;
  constructor(public payload: Dish[]) {}
}

export class GetDish implements Action {
  public readonly type = EDishActions.GetDish;
  constructor(public payload: number) {}
}

export class GetDishSuccess implements Action {
  public readonly type = EDishActions.GetDishSuccess;
  constructor(public payload: Dish) {}
}

// export class DishAdd implements ActionEx {
//   readonly type = EDishActions.Add;
//   constructor(public payload: any) {}
// }

// export class DishRemove implements ActionEx {
//   readonly type = EDishActions.Remove;
//   constructor(public payload: any) {}
// }

export type DishActions =
  | GetDishes
  | GetDishesSuccess
  | GetDish
  | GetDishSuccess;
// | DishAdd
// | DishRemove;

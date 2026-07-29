import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { select, Store } from '@ngrx/store';
import { of } from 'rxjs';
import { map, switchMap, withLatestFrom } from 'rxjs/operators';
import { Dish } from '../../models/dish';
import { DishService } from '../../services/dish.service';
import { IAppState } from '../app.state';
import {
  EDishActions,
  GetDish,
  GetDishes,
  GetDishesSuccess,
  GetDishSuccess,
} from './dish.actions';
import { selectDishList } from './dish.selectors';

@Injectable()
export class DishEffects {
  constructor(
    private dishService: DishService,
    private actions$: Actions,
    private store: Store<IAppState>
  ) {}

  @Effect()
  getDishes$ = this.actions$.pipe(
    ofType<GetDishes>(EDishActions.GetDishes),
    switchMap(() => this.dishService.getDishes()),
    switchMap((dishes: Dish[]) => of(new GetDishesSuccess(dishes)))
  );

  @Effect()
  getDish$ = this.actions$.pipe(
    ofType<GetDish>(EDishActions.GetDish),
    map((action) => action.payload),
    withLatestFrom(this.store.pipe(select(selectDishList))),
    switchMap(([id, dishes]) => {
      const selectedDish = dishes.filter((dish) => dish.dishId === +id)[0];
      return of(new GetDishSuccess(selectedDish));
    })
  );
}

import { RouterReducerState } from '@ngrx/router-store';
import { createFeatureSelector } from '@ngrx/store';
import * as auth from './auth/auth.reducers';
import { initialAuthState } from './auth/auth.reducers';
import { IDishState, initialDishState } from './dish/dish.state';

export interface IAppState {
  router?: RouterReducerState;
  dishes: IDishState;
  authState: auth.State;
}

export const initialAppState: IAppState = {
  dishes: initialDishState,
  authState: initialAuthState,
};

export const selectAuthState = createFeatureSelector<IAppState>('authState');

export function getInitialState(): IAppState {
  return initialAppState;
}

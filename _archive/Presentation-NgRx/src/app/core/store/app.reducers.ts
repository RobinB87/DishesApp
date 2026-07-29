import { routerReducer } from '@ngrx/router-store';
import { ActionReducerMap } from '@ngrx/store';
import { IAppState } from './app.state';
import { authReducers } from './auth';
import { dishReducers } from './dish/dish.reducers';

export const appReducers: ActionReducerMap<IAppState, any> = {
  router: routerReducer,
  dishes: dishReducers,
  authState: authReducers,
};

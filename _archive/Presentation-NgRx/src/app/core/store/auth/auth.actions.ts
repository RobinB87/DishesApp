import { Action } from '@ngrx/store';

export enum EAuthActionTypes {
  LOGIN = '[Auth] Login',
  LOGIN_SUCCESS = '[Auth] Login Success',
  LOGIN_FAILURE = '[Auth] Login Failure',

  LOGOUT = '[Auth] LogOut',

  GET_STATUS = '[Auth] GetStatus',
}

/** Login actions */
export class LogIn implements Action {
  readonly type = EAuthActionTypes.LOGIN;
  constructor(public payload: any) {}
}

export class LogInSuccess implements Action {
  readonly type = EAuthActionTypes.LOGIN_SUCCESS;
  constructor(public payload: any) {}
}

export class LogInFailure implements Action {
  readonly type = EAuthActionTypes.LOGIN_FAILURE;
  constructor(public payload: any) {}
}

/** LogOut actions */
export class LogOut implements Action {
  readonly type = EAuthActionTypes.LOGOUT;
}

/** Get status actions */
export class GetStatus implements Action {
  readonly type = EAuthActionTypes.GET_STATUS;
}

export type All = LogIn | LogInSuccess | LogInFailure | LogOut | GetStatus;

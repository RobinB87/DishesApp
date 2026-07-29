import { User } from '../../models/auth';
import { All, EAuthActionTypes } from './auth.actions';

export interface State {
  // is a user authenticated?
  isAuthenticated: boolean;
  // if authenticated, there should be a user object
  user: User | null;
  // error message
  errorMessage: string | null;
}

export const initialAuthState: State = {
  isAuthenticated: false,
  user: null,
  errorMessage: null,
};

export function authReducers(state = initialAuthState, action: All): State {
  switch (action.type) {
    case EAuthActionTypes.LOGIN_SUCCESS: {
      return {
        ...state,
        isAuthenticated: true,
        user: {
          token: action.payload.token,
          email: action.payload.email,
        },
        errorMessage: null,
      };
    }
    case EAuthActionTypes.LOGIN_FAILURE: {
      return {
        ...state,
        errorMessage: 'Incorrect email and/or password.',
      };
    }
    case EAuthActionTypes.LOGOUT: {
      return initialAuthState;
    }
    default: {
      return state;
    }
  }
}

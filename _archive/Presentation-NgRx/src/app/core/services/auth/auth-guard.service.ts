import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IAppState, selectAuthState } from '../../store';
import { AuthService } from './auth.service';

/** The Auth Guard Service. The CanActivate route guard interface implements the guard itself */
@Injectable()
export class AuthGuardService implements CanActivate {
  getState: Observable<any>;
  isAuthenticated: false;
  user = null;
  errorMessage = null;

  constructor(
    private store: Store<IAppState>,
    public auth: AuthService,
    public router: Router
  ) {
    this.getState = this.store.select(selectAuthState);
  }

  // In the canActivate method, check to see if a token is in localStorage.
  // Return the appropriate boolean, and (if necessary) redirect the user.
  canActivate(): boolean {
    this.getState.subscribe((state) => {
      this.isAuthenticated = state.isAuthenticated;
      this.user = state.user;
      this.errorMessage = state.errorMessage;
    });

    if (this.auth.getToken() && this.isAuthenticated) {
      return true;
    }

    this.router.navigateByUrl('/log-in');
    return false;
  }
}

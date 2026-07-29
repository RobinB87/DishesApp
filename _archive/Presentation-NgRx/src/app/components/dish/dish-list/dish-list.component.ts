import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IAppState, selectAuthState } from '../../../core/store';
import { LogOut } from '../../../core/store/auth';
import { GetDishes, selectDishList } from '../../../core/store/dish';

/* The dish list component */
@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css'],
})
export class DishListComponent implements OnInit {
  getState: Observable<any>;
  isAuthenticated: false;
  user = null;
  errorMessage = null;

  dishes$ = this.store.pipe(select(selectDishList));

  constructor(private store: Store<IAppState>, private router: Router) {
    this.getState = this.store.select(selectAuthState);
  }

  ngOnInit() {
    this.getState.subscribe((state) => {
      this.isAuthenticated = state.isAuthenticated;
      this.user = state.user;
      this.errorMessage = state.errorMessage;
    });

    this.store.dispatch(new GetDishes());
  }

  logOut(): void {
    this.store.dispatch(new LogOut());
  }

  navigateToDish(dishId: number) {
    this.router.navigate(['dish', dishId]);
  }

  // removeDish(dishIndex) {
  //   this.store.dispatch(new DishRemove(dishIndex));
  // }
}

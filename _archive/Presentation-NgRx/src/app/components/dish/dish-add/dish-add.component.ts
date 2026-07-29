import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Dish } from '../../../core/models/dish';

/* The dish add component */
@Component({
  selector: 'app-dish-add',
  templateUrl: './dish-add.component.html',
  styleUrls: ['./dish-add.component.css'],
})
export class DishAddComponent {
  dishes: Observable<Dish[]>;

  constructor(private store: Store<{ dishes: Dish[] }>) {
    this.dishes = store.pipe(select('dishes'));
  }

  // AddDish(dishName: string) {
  //   const dish = new Dish();
  //   dish.name = dishName;
  //   this.store.dispatch(new DishAdd(dish));
  // }
}

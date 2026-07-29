import { Component, OnInit } from "@angular/core";
import { Dish } from "./shared/dish.model";
import { DishService } from "./shared/dish.service";

@Component({
  selector: "app-dishes",
  templateUrl: "./dishes.component.html",
  styleUrls: ["./dishes.component.css"],
})
export class DishesComponent implements OnInit {
  title: string = "Dishes overview";
  dishes: Dish[] = [];

  constructor(private dishService: DishService) {}

  calculateSumOfNumberOfIngredients(dish) {
    let totalNumberOfIngredients = dish.ingredients.length;
    return totalNumberOfIngredients;
  }

  calculateSumOfIngredientPrices(dish) {
    return dish.ingredients.reduce(
      (p, n) => p + n.pricePerUnit * n.quantity,
      0
    );
  }

  ngOnInit() {
    this.dishService.getDishes().subscribe((dishes) => {
      this.dishes = dishes;
    });
  }
}

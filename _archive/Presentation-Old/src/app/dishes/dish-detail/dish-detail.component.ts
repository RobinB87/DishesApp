import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { DishService } from '../shared/dish.service';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';

import { MatDialog, MatDialogConfig } from '@angular/material';
import { DishDeleteModalComponent } from '../dish-delete-modal/dish-delete-modal.component';

@Component({
  selector: 'app-dish-detail',
  templateUrl: './dish-detail.component.html',
  styleUrls: ['./dish-detail.component.css']
})
export class DishDetailComponent implements OnInit, OnDestroy {

  private dish: any;
  private dishId: number;
  private sub: Subscription;

  constructor(private dishService: DishService,
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog) { }

  openModal() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      id: this.dishId,
      title: this.dish.name
    };

    const dialogRef = this.dialog.open(DishDeleteModalComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      console.log("Dialog was closed")
      console.log(result)
    });
  }

  calculateSumOfNumberOfIngredients() {
    let totalNumberOfIngredients = this.dish.ingredients.length;
    return totalNumberOfIngredients;
  }

  calculateSumOfIngredientPrices() {
    let totalIngredientPrice = 0;
    let total = 0;

    for (var i = 0; i < this.dish.ingredients.length; i++) {
      totalIngredientPrice = this.dish.ingredients[i].pricePerUnit * this.dish.ingredients[i].quantity;

      total += totalIngredientPrice;
    }
    return total;
  }

  ngOnInit() {
    // get route data
    this.sub = this.route.params.subscribe(
      params => {
        this.dishId = params['dishId'];

        // get dish
        this.dishService.getDish(this.dishId)
          .subscribe(dish => {
            this.dish = dish;
          });
      }
    );
  }

  ngOnDestroy(): void {
    if (this.sub) {
    this.sub.unsubscribe();
    }
  }
}

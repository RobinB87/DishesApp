import { Component, OnInit, Input } from '@angular/core';
import { Ingredient } from './shared/ingredient.model';
import { IngredientService } from './shared/ingredient.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { IngredientDeleteModalComponent } from '../ingredients/ingredient-delete-modal/ingredient-delete-modal.component';
import { Dish } from '../shared/dish.model';
import { DishService } from '../shared/dish.service';

@Component({
  selector: 'app-ingredients',
  templateUrl: './ingredients.component.html',
  styleUrls: ['./ingredients.component.css']
})
export class IngredientsComponent implements OnInit {

  private dish: Dish;
  private ingredient: Ingredient;
  private sub: Subscription;

  @Input() ingredients: Ingredient[];

  constructor(private ingredientService: IngredientService,
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog) { }

  openModal(item, items) {
    console.log('testlog: ' + item);
    console.log('testlog: ' + items);

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
      dialogConfig.data = {
        item: item,
        items: items,
      };

    const dialogRef = this.dialog.open(IngredientDeleteModalComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      console.log("Dialog was closed")
      console.log(result)
    });
  }

  ngOnInit() {
    }
  }

import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { IngredientService } from '../shared/ingredient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Ingredient } from '../shared/ingredient.model';
import { Subscription } from 'rxjs';
import { DishService } from '../../shared/dish.service';
import { Dish } from '../../shared/dish.model';

@Component({
  selector: 'app-ingredient-delete-modal',
  templateUrl: './ingredient-delete-modal.component.html',
  styleUrls: ['./ingredient-delete-modal.component.css']
})
export class IngredientDeleteModalComponent implements OnInit {

  modalTitle: string;
  private ingredient: Ingredient;
  private ingredients: Ingredient[];
  private sub: Subscription;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private ingredientService: IngredientService,
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog) {
    this.modalTitle = data.item.name;
    this.ingredient = data.item; // an item is the ingredient
    this.ingredients = data.items; // items is the list of ingredients
    console.log(data)
  }

  deleteIngredientFromDish(ingredient): void {
    console.log('deleteIngredientFromDish log: ' + ingredient.name);
    console.log('deleteIngredientFromDish log: ' + this.ingredients);

    this.ingredientService.deleteIngredientFromDish(ingredient.dishId, ingredient.ingredientId)
      .subscribe(ingredient => {
      })

    const index = this.ingredients.indexOf(ingredient);
    this.ingredients.splice(index, 1);

    console.log('ingredient ' + ingredient.name + ' with id ' + ingredient.ingredientId + ' has been removed.');

    //this.router.navigateByUrl('/dishes/' + ingredient.dishId);
  }

  ngOnInit() {
  }
}

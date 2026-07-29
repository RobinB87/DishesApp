import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Ingredient } from '../shared/ingredient.model';
import { Subscription } from 'rxjs';
import { IngredientForUpdate } from '../shared/ingredient-for-update.model';
import { IngredientService } from '../shared/ingredient.service';
import { ActivatedRoute, Router } from '@angular/router';
import { compare } from 'fast-json-patch';
import { Dish } from '../../shared/dish.model';

@Component({
  selector: 'app-ingredient-update',
  templateUrl: './ingredient-update.component.html',
  styleUrls: ['./ingredient-update.component.css']
})
export class IngredientUpdateComponent implements OnInit {
  public ingredientForm: FormGroup;
  private dishId: number;
  private ingredient: Ingredient;
  private ingredientId: number;
  private sub: Subscription;
  private originalIngredientForUpdate: IngredientForUpdate;

  constructor(private ingredientService: IngredientService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder) { }

ngOnInit() {
  this.ingredientForm = this.formBuilder.group({
    name: ['', [Validators.required, Validators.maxLength(30)]],
    pricePerUnit: ['', [Validators.required, Validators.max(100), Validators.pattern('[0-9.]*')]],
    quantity: ['', [Validators.required, Validators.max(100), Validators.pattern('[0-9.]*')]]
  });

  // get route data (ingredientId)
  this.sub = this.route.params.subscribe(
    params => {
      this.dishId = params['dishId'];
        this.ingredientId = params['ingredientId'];

      // load ingredient
      this.ingredientService.getIngredientForDish(this.dishId, this.ingredientId)
        .subscribe(ingredient => {
          this.ingredient = ingredient;
          this.updateIngredientForm();
          this.originalIngredientForUpdate = this.ingredientForm.value;
        })
    }
  );
}

ngOnDestroy(): void {
  this.sub.unsubscribe();
}

  // TODO what is this exactly? 
  private updateIngredientForm(): void {
  this.ingredientForm.patchValue({
    name: [''],
    pricePerUnit: ['']
  });
}

saveIngredient(): void {
  if (this.ingredientForm.dirty && this.ingredientForm.valid) {
    let changedIngredientForUpdate = this.ingredientForm.value;

    let patchDocument = compare(this.originalIngredientForUpdate, changedIngredientForUpdate);

    this.ingredientService.partiallyUpdateIngredient(this.dishId, this.ingredientId, patchDocument)
      .subscribe(
        () => {
          this.router.navigateByUrl('/dishes/' + this.dishId);
        });
  }
}
}

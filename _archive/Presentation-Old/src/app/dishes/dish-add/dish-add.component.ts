import { Component, OnInit } from "@angular/core";
import { FormArray, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ValidationErrorHandler } from "src/app/shared/validation-error-handler";
import { CustomValidators } from "../../shared/custom-validators";
import { IngredientSingleComponent } from "../ingredients/ingredient-single/ingredient-single.component";
import { DishService } from "../shared/dish.service";
// import { OpenIdConnectService } from 'src/app/shared/open-id-connect.service';

@Component({
  selector: "app-dish-add",
  templateUrl: "./dish-add.component.html",
  styleUrls: ["./dish-add.component.css"],
})
export class DishAddComponent implements OnInit {
  public dishForm: FormGroup;
  // private isAdmin: boolean = this.openIdConnectService.user.profile.role === 'Administrator';

  constructor(
    private dishService: DishService,
    private formBuilder: FormBuilder,
    private router: Router
  ) // private openIdConnectService: OpenIdConnectService
  {}

  ngOnInit() {
    // define dishForm (with empty default values)
    this.dishForm = this.formBuilder.group(
      {
        name: [
          "",
          [
            Validators.required,
            Validators.maxLength(30),
            Validators.pattern("[a-zA-Z :]*"),
          ],
        ],
        country: [""],
        ingredients: this.formBuilder.array([]),
        recipe: ["", [Validators.required, Validators.minLength(50)]],
      },
      { validator: CustomValidators.DishNameEqualsCountryValidator }
    );
  }

  // method for the button on the dishform to show inputfields for ingredient
  addIngredient(): void {
    let ingredientsFormArray = this.dishForm.get("ingredients") as FormArray;
    ingredientsFormArray.push(IngredientSingleComponent.createIngredient());
  }

  addDish(): void {
    if (this.dishForm.dirty && this.dishForm.valid) {
      // new dish is an object created by the values of the input fields of the dishForm.
      let newDish = this.dishForm.value;
      let ingredients = this.dishForm.value.ingredients;
      let ingredientNames = [];

      for (let ingredient of ingredients) {
        if (ingredientNames.indexOf(ingredient.name) > -1) {
          window.alert(
            ingredient.name +
              " already exists for " +
              newDish.name +
              ", please choose a different ingredient."
          );
          return;
        }
        ingredientNames.push(ingredient.name);
      }

      this.dishService.addDish(newDish).subscribe(
        () => {
          this.router.navigateByUrl("/dishes");
        },
        // whenever a validation error occurs, we call in to handle validationError passing through the validationResult.
        (validationResult) => {
          ValidationErrorHandler.handleValidationErrors(
            this.dishForm,
            validationResult
          );
        }
      );
    }
  }
}

// function to get the list of existing dishes. toPromise makes it async.
// getExistingDishes() {
//   let existingDishes = this.dishService.getDishes().toPromise();
//   return existingDishes;
// }
// }

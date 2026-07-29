import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { compare } from "fast-json-patch";
import { Subscription } from "rxjs";
import { CustomValidators } from "../../shared/custom-validators";
import { DishForUpdate } from "../shared/dish-for-update.model";
import { Dish } from "../shared/dish.model";
import { DishService } from "../shared/dish.service";

@Component({
  selector: "app-dish-update",
  templateUrl: "./dish-update.component.html",
  styleUrls: ["./dish-update.component.css"],
})
export class DishUpdateComponent implements OnInit {
  public dishForm: FormGroup;
  private dish: Dish;
  private dishId: number;
  private sub: Subscription;
  private originalDishForUpdate: DishForUpdate;

  constructor(
    private dishService: DishService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    // define dishForm (with empty default values)
    // add validators
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
        recipe: ["", [Validators.required, Validators.minLength(50)]],

        // here you ensure the custom validator is used (next, add it to the html)
      },
      { validator: CustomValidators.DishNameEqualsCountryValidator }
    );

    // get route data (dishId)
    this.sub = this.route.params.subscribe((params) => {
      this.dishId = params["dishId"];

      // load dish
      this.dishService.getDish(this.dishId).subscribe((dish) => {
        this.dish = dish;
        this.updateDishForm();
        this.originalDishForUpdate = this.dishForm.value;
      });
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  private updateDishForm(): void {
    this.dishForm.patchValue({
      name: [""],
      country: [""],
      recipe: [""],
    });
  }

  // dirty means the user has ALREADY interacted with the input value.
  // only save when dishForm is valid (also add validation to amongst others save button in html)
  saveDish(): void {
    if (this.dishForm.dirty && this.dishForm.valid) {
      let changedDishForUpdate = this.dishForm.value;

      let patchDocument = compare(
        this.originalDishForUpdate,
        changedDishForUpdate
      );

      this.dishService
        .partiallyUpdateDish(this.dishId, patchDocument)
        .subscribe(() => {
          this.router.navigateByUrl("/dishes");
        });
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { IngredientSingleComponent } from '../ingredient-single/ingredient-single.component';
import { IngredientService } from '../shared/ingredient.service';

@Component({
  selector: 'app-ingredient-add',
  templateUrl: './ingredient-add.component.html',
  styleUrls: ['./ingredient-add.component.css']
})
export class IngredientAddComponent implements OnInit {

  private sub: Subscription;
  private dishId: number;
  public ingredientCollectionForm: FormGroup;

  constructor(private ingredientService: IngredientService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.ingredientCollectionForm = this.formBuilder.group({
      ingredients: this.formBuilder.array([])
    });

    this.addIngredient();

    // get route data (dishId)
    this.sub = this.route.params.subscribe(
      params => {
        this.dishId = params['dishId'];
      }
    );
  }

  addIngredient(): void {
    let ingredientsFormArray = this.ingredientCollectionForm.get('ingredients') as FormArray;
    ingredientsFormArray.push(IngredientSingleComponent.createIngredient());
  }


  //TODO: add dishname binding in window.alert
  async addIngredients() {
    if (this.ingredientCollectionForm.dirty && this.ingredientCollectionForm.valid
      && this.ingredientCollectionForm.value.ingredients.length) {

        let existingIngredientCollection = await this.getExistingIngredients();
        let newIngredientCollection = this.ingredientCollectionForm.value.ingredients;
        let messages: string[] = [];

        for (let newIngredient of newIngredientCollection){

          for (let existingIngredient of existingIngredientCollection){
            if (newIngredient.name != existingIngredient.name) {
              continue;
            }

            // if match, create message for summed up quantity
            messages.push(existingIngredient.name);
            break;
          }
        }

        if (messages != null)
        {
          var newMessages = messages.join(",");
          var newMessagesWithSpace = newMessages.replace(",", ", ");
          window.alert("Duplicate ingredients were found for: " + newMessagesWithSpace + ". Quantities are summed up.");
        }

        this.ingredientService.addIngredientCollection(this.dishId, newIngredientCollection)
          .subscribe(
            () => {
              this.router.navigateByUrl('/dishes/' + this.dishId);
            });
        }
    }

        // for (var i = 0; i < ingredientCollection.length; i++) {
        //   if (ingredientNames.indexOf(ingredientCollection[i].name) > -1) {
        //     window.alert(ingredientCollection[i].name + " already exists for " + dishName + ", please choose a different ingredient.");
        //     return;
        //   }
        //   ingredientNames.push(ingredientCollection[i].name);
        // }



        // DIT WEER AANZETTEN
        // this.ingredientService.addIngredientCollection(this.dishId, ingredientCollection)
        //   .subscribe(
        //     () => {
        //       this.router.navigateByUrl('/dishes/' + this.dishId);
        //     });



  async getDishName() {
    let dish = await this.ingredientService.getDish(this.dishId).toPromise();
    return dish.name;
  }

  async getExistingIngredients() {
    let ingredients = await this.ingredientService.getIngredients(this.dishId).toPromise();
    return ingredients;
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}

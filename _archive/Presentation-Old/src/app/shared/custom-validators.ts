import { AbstractControl } from '@angular/forms';

export class CustomValidators {

  static DishNameEqualsCountryValidator(control: AbstractControl) {
    let name = control.get('name');
    let country = control.get('country');

    // if name is not equal, return null (we are good to go).
    if (name.value != country.value) {
      return null;
    }

    // return true below means there is a validation error. Next, add custom validator to component.ts file.
    return { 'dishNameEqualsCountry': true }
  }

  //static DishAlreadyExistsValidator(control: AbstractControl) {

  //  let name = control.get('name');
  //  let dishes = control.get('dishes');

  //  for (var dish in dishes) {
  //    if (dish.name != name) {
  //      return null;
  //    }
  //  }
  //  return { 'dishAlreadyExists': true }
  //}
}

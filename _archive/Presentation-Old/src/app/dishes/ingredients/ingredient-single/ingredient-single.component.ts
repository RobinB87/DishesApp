import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-ingredient-single',
  templateUrl: './ingredient-single.component.html',
  styleUrls: ['./ingredient-single.component.css']
})
export class IngredientSingleComponent implements OnInit {

  @Input() public ingredientIndex: number;
  @Input() public ingredient: FormGroup;

  @Output() public removeIngredientClicked: EventEmitter<number> = new EventEmitter<number>();

  constructor() { }

  static createIngredient() {
    return new FormGroup({
      name: new FormControl([], [Validators.required, Validators.maxLength(30), Validators.pattern('[a-zA-Z :]*')]),
      pricePerUnit: new FormControl([], [Validators.required, Validators.max(100), Validators.pattern('[0-9.]*')]),
      quantity: new FormControl([], [Validators.required, Validators.max(100), Validators.pattern('[0-9.]*')]),
    });
  }

  ngOnInit() {
  }
}

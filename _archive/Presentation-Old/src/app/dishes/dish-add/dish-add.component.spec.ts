import { DishAddComponent } from "./dish-add.component";
import { FormBuilder, FormsModule, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DishService } from "../shared/dish.service";
import { Component, Input } from "@angular/core";
import { RouterTestingModule } from '@angular/router/testing';

describe('DishAddComponent', () => {
  let component: DishAddComponent;
  let fixture: ComponentFixture<DishAddComponent>;
  let dishServiceMock: any;

  // mock singleingredientcomponent
  @Component({
    selector: 'app-ingredient-single',
    template: '<div></div>',
  })
  class MockSingleIngredientComponent {
  @Input() public ingredientIndex: number;
  @Input() public ingredient: FormGroup;
  }

  // create new instance of FormBuilder
  const formBuilder: FormBuilder = new FormBuilder();

  beforeEach(async(() => {
    dishServiceMock = {
      addDish: jest.fn().mockName('dishServiceMock.addDish'),
      addIngredient: jest.fn().mockName('dishServiceMock.addIngredient')
    };

    TestBed.configureTestingModule({
      imports: [ ReactiveFormsModule, FormsModule, RouterTestingModule.withRoutes([])],
      declarations: [ DishAddComponent, MockSingleIngredientComponent ],
      providers: 
      [ 
        { provide: FormBuilder, useValue: formBuilder },
        { provide: DishService, useValue: dishServiceMock }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    // create component and test fixture
    fixture = TestBed.createComponent(DishAddComponent);

    // get test component from the fixture
    component = fixture.componentInstance;

    // ..
    fixture.detectChanges();
  });

  // unit tests
  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


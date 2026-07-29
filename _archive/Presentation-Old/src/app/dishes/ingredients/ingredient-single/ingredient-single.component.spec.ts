import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientSingleComponent } from './ingredient-single.component';

describe('IngredientSingleComponent', () => {
  let component: IngredientSingleComponent;
  let fixture: ComponentFixture<IngredientSingleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IngredientSingleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientSingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

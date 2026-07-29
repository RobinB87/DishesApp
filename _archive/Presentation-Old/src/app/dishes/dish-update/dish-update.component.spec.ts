import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DishUpdateComponent } from './dish-update.component';

describe('DishUpdateComponent', () => {
  let component: DishUpdateComponent;
  let fixture: ComponentFixture<DishUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DishUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DishUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

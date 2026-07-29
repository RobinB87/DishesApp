import { TestBed, inject } from '@angular/core/testing';

import { IngredientService } from './ingredient.service';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('IngredientService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IngredientService],
      imports: [RouterTestingModule, HttpClientTestingModule]
    });
  });

  it('should be created', inject([IngredientService], (service: IngredientService) => {
    expect(service).toBeTruthy();
  }));
});

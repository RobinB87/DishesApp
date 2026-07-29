import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../../../shared/base.service';
import { Observable } from 'rxjs/internal/Observable';
import { Ingredient } from './ingredient.model';
import { Operation } from 'fast-json-patch';

@Injectable()
export class IngredientService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  // Get
  getIngredients(dishId: number): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(`${this.apiUrl}/dishes/${dishId}/ingredients`);
  }

  getIngredientForDish(dishId: number, ingredientId: number): Observable<Ingredient> {
    return this.http.get<Ingredient>(`${this.apiUrl}/dishes/${dishId}/ingredients/${ingredientId}`)
  }

  //TODO: kan weg?
  //Get dish to obtain dish name
  getDish(dishId: number): Observable<Ingredient> {
    return this.http.get<Ingredient>(`${this.apiUrl}/dishes/${dishId}`)
  }

  // Add ingredient
  addIngredient(dishId: number, ingredientToAdd: Ingredient): Observable<Ingredient> {
    return this.http.post<Ingredient>(`${this.apiUrl}/dishes/${dishId}/ingredients`, ingredientToAdd);
  }

  // Add ingredientCollection
  addIngredientCollection(dishId: number, ingredientsToAdd: Ingredient[]): Observable<Ingredient[]> {
    return this.http.post<Ingredient[]>(`${this.apiUrl}/dishes/${dishId}/ingredientcollections`, ingredientsToAdd);
  }

  // Edit
  partiallyUpdateIngredient(dishId: number, ingredientId: number, patchDocument: Operation[]): Observable<any> {
    return this.http.patch(`${this.apiUrl}/dishes/${dishId}/ingredients/${ingredientId}`, patchDocument,
      { headers: { 'Content-Type': 'application/json-patch+json' } });
  }

  // Delete
  deleteIngredientFromDish(dishId: number, ingredientId: number): Observable<Ingredient> {
    return this.http.delete<Ingredient>(`${this.apiUrl}/dishes/${dishId}/ingredients/${ingredientId}`)
  }
}

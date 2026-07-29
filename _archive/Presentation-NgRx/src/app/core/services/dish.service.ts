import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Dish } from '../models/dish';

/** The dish service */
@Injectable({
  providedIn: 'root',
})
export class DishService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  /** Service call to get all dishes */
  public getDishes(): Observable<Dish[]> {
    return this.http.get<Dish[]>(`${this.apiUrl}/dish`);
  }

  /** Service call to get a dish by id */
  public getDish(id: number): Observable<Dish> {
    return this.http.get<Dish>(`${this.apiUrl}/dish/${id}`);
  }

  /** Service call to create a dish */
  addDish(dishToAdd: Dish): Observable<Dish> {
    return this.http.post<Dish>(`${this.apiUrl}/dish`, dishToAdd);
  }

  /** Service call to delete a dish */
  deleteDish(dishId: number): Observable<Dish> {
    return this.http.delete<Dish>(`${this.apiUrl}/dish/${dishId}`);
  }
}

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Operation } from "fast-json-patch";
import { Observable } from "rxjs";
import { BaseService } from "../../shared/base.service";
import { Dish } from "./dish.model";

@Injectable()
export class DishService extends BaseService {
  httpClient: any;

  constructor(private http: HttpClient) {
    super();
  }

  // Get
  getDishes(): Observable<Dish[]> {
    return this.http.get<Dish[]>(`${this.apiUrl}/dish`);
  }

  getDish(dishId: number): Observable<Dish> {
    return this.http.get<Dish>(`${this.apiUrl}/dish/${dishId}`);
  }

  // Add
  addDish(dishToAdd: Dish): Observable<Dish> {
    return this.http.post<Dish>(`${this.apiUrl}/dish`, dishToAdd);
  }

  // Update (send patchdocument to API)
  partiallyUpdateDish(
    dishId: number,
    patchDocument: Operation[]
  ): Observable<any> {
    return this.http.patch(`${this.apiUrl}/dish/${dishId}`, patchDocument, {
      headers: { "Content-Type": "application/json-patch+json" },
    });
  }

  // Delete
  deleteDish(dishId: number): Observable<Dish> {
    return this.http.delete<Dish>(`${this.apiUrl}/dish/${dishId}`);
  }
}

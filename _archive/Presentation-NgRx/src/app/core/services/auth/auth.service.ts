import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { User } from '../../models/auth';

@Injectable()
export class AuthService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getToken(): string {
    return localStorage.getItem('token');
  }

  logIn(email: string, password: string): Observable<any> {
    const url = `${this.apiUrl}/login`;
    return this.http.post<User>(url, { email, password });
  }

  signUp(email: string, password: string): Observable<User> {
    const url = `${this.apiUrl}/register`;
    return this.http.post<User>(url, { email, password });
  }

  getStatus(): Observable<User> {
    const url = `${this.apiUrl}/login/status`;
    return this.http.get<User>(url);
  }
}

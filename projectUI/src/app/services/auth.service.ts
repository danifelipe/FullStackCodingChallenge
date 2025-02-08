import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticated = false;

  login(username: string, password: string): Observable<boolean> {
    // Simulate API call - in real app, this would call a backend service
    if (username === 'admin' && password === 'admin') {
      this.isAuthenticated = true;
      return of(true);
    }
    return of(false);
  }

  logout(): void {
    this.isAuthenticated = false;
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated;
  }
}
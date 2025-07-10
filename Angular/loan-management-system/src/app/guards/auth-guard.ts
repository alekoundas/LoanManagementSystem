import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const isAuthenticated = false; // Replace with actual auth logic
    if (isAuthenticated) {
      return true;
    } else {
      this.router.navigate(['/home']);
      return false;
    }
  }
}

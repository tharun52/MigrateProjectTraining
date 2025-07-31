import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth-service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isLoggedIn = this.auth.isLoggedIn();
    const expectedRole = route.data['role'];
    const userRole = this.auth.getRole();

    if (isLoggedIn && userRole === expectedRole) {
      return true;
    }

    if (userRole === 'Admin') {
      this.router.navigate(['/admin']);
    } else if (userRole === 'User') {
      this.router.navigate(['/home']);
    } else {
      this.router.navigate(['/login']);
    }

    return false;
  }
}
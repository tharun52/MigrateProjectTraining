import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {
  constructor(public auth: AuthService) {}
  logout() {
    this.auth.logout();
  }
}

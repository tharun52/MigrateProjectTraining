import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  loginForm: FormGroup;
  loading: boolean = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {
    this.loginForm = new FormGroup({
      un: new FormControl('', { validators: Validators.required }),
      pass: new FormControl('', Validators.required),
    });
  }

  public get un() {
    return this.loginForm.get('un');
  }

  public get pass() {
    return this.loginForm.get('pass');
  }
  handleLogin() {
    if (this.loginForm.invalid) return;

    this.loading = true;
    const username = this.un?.value;
    const password = this.pass?.value;

    console.log('Attempting login with:', username, password);

    this.auth.login(username, password).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('refreshToken', res.refreshToken);

        const role = this.auth.getRole();

        if (role === 'Admin') {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/home']);
        }

        this.loading = false;
      },
      error: (err) => {
        this.error = err.error?.message || 'Login failed. Please try again.';
        this.loading = false;
      }
    });
  }
}

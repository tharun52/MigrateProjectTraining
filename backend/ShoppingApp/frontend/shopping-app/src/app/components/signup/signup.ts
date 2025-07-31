import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css'
})
export class Signup {
  signupForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {
    this.signupForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required])
    });
  }

  get username() {
    return this.signupForm.get('username');
  }

  get password() {
    return this.signupForm.get('password');
  }

  handleSignup() {
    if (this.signupForm.invalid) return;

    this.loading = true;
    this.error = null;

    const { username, password } = this.signupForm.value;

    this.auth.signup(username, password).subscribe({
      next: () => {
        this.router.navigate(['/login']);
        this.loading = false;
      },
      error: (err) => {
        this.error = err.error?.message || 'Signup failed.';
        this.loading = false;
      }
    });
  }
}
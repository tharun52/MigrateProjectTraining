import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up-admin',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './sign-up-admin.html',
  styleUrl: './sign-up-admin.css'
})
export class SignUpAdmin {
  signupForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(private authService: AuthService, private router: Router) {
    this.signupForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('', Validators.required),
      secretKey: new FormControl('', Validators.required),
    },
      { validators: this.passwordMatchValidator }
    );
  }

  get username() {
    return this.signupForm.get('username');
  }

  get password() {
    return this.signupForm.get('password');
  }

  get confirmPassword() {
    return this.signupForm.get('confirmPassword');
  }

  get secretKey() {
    return this.signupForm.get('secretKey');
  }

  passwordMatchValidator(form: AbstractControl) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  handleSignup() {
    if (this.signupForm.invalid) return;

    const { username, password, secretKey } = this.signupForm.value;

    this.loading = true;
    this.error = null;

    this.authService.signupAsAdmin(username, password, secretKey).subscribe({
      next: () => {
        this.authService.login(username, password).subscribe({
          next: (res) => {
            localStorage.setItem('token', res.token);
            localStorage.setItem('refreshToken', res.refreshToken);

            this.router.navigate(['/admin']);
            this.loading = false;
          },
          error: (err) => {
            this.error = err.error?.message || 'Auto-login failed.';
            this.loading = false;
          }
        });
      },
      error: (err) => {
        this.loading = false;
        this.error = err.error?.message || 'Signup failed. Please try again.';
      }
    });
  }
}
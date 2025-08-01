import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
  AbstractControl
} from '@angular/forms';
import { CaptchaService } from '../../services/captcha-service';
import { environment } from '../../../environments/environment';

declare const grecaptcha: any;

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css'
})
export class Signup {
  signupForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private auth: AuthService,
    private captchaService: CaptchaService,
    private router: Router
  ) {
    this.signupForm = new FormGroup(
      {
        username: new FormControl('', Validators.required),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(6)
        ]),
        confirmPassword: new FormControl('', Validators.required)
      },
      { validators: this.passwordMatchValidator }
    );
  }
  passwordMatchValidator(form: AbstractControl) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
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

  handleSignup() {
    if (this.signupForm.invalid) return;

    this.loading = true;
    this.error = null;

    grecaptcha.ready(() => {
      grecaptcha
        .execute(environment.captchakey, { action: 'submit' })
        .then((token: string) => {
          this.captchaService.validateCaptcha(token).subscribe({
            next: () => {
              console.log('Executing reCAPTCHA...');
              const { username, password } = this.signupForm.value;

              this.auth.signup(username, password).subscribe({
                next: () => {
                  this.auth.login(username, password).subscribe({
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
                  this.error = err.error?.message || 'Signup failed.';
                  this.loading = false;
                }
              });
            },
            error: () => {
              this.error = 'Captcha verification failed.';
              this.loading = false;
            }
          });
        });
    });
  }
}
